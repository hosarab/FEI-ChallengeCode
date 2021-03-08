using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Insight.Application.Common.Exceptions;
using Insight.Application.Common.Helpers;
using Insight.Application.Interfaces;
using Insight.Application.Models;
using Insight.Application.PostCodesFeatures.Queries;
using Insight.Application.Wrappers;
using Newtonsoft.Json;

namespace Insight.Infrastructure.Services
{
    public class PostcodesService : IPostcodeService
    {

        private readonly IHttpService _httpService;
        private readonly ICacheService _cacheSrv;

        public PostcodesService(IHttpService httpService, ICacheService cacheSrv)
        {
            _httpService = httpService;
            _cacheSrv = cacheSrv;
        }

        //This endpoint should receive a postcode and return the following object if the postcode is valid.
        public async Task<PostcodeDetailsViewModel> LookupByPostCode(string postcode)
        {
            return await _cacheSrv.GetCache(() => LookupBy(postcode));
        }

        public async Task<bool> ValidateAsync(string postcode)
        {
            return await IsValidPostcode(postcode);
        }

        private async Task<PostcodeDetailsViewModel> LookupBy(string postcode)
        {
            var url = $"/postcodes/{PostcodeFormatter.FormatPostcode(postcode)}";

            var response = await _httpService.GetAsync<PostcodeLookupResponse>(url);

            return new PostcodeDetailsViewModel
            {
                Postcode = response.Result.Postcode,
                Coordinates = new CoordinatesModel
                {
                    Latitude = response.Result.Latitude,
                    Longitude = response.Result.Longitude
                }
            };
        }

        public async Task<bool> IsValidPostcode(string postcode)
        {
            try
            {
                using (var httpClient = new HttpClient())
                {
                    httpClient.BaseAddress = new Uri("https://api.postcodes.io");

                    var path = $"/postcodes/{postcode}/validate";

                    var response = await httpClient.GetAsync(path);

                    if (response.IsSuccessStatusCode)
                    {
                        var bodyString = await response.Content.ReadAsStringAsync();
                        var body = JsonConvert.DeserializeObject<PostcodesApiValidateResponse>(bodyString);
                        return body.Result;
                    }

                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<PostcodesListViewModel> BulkLookup(IEnumerable<string> postcodes)
        {
            if (!postcodes.Any() || postcodes == null) throw new CustomValidationException();

            PostcodesListViewModel postcodesWithDetails = new PostcodesListViewModel
            {
                Postcodes = new List<PostcodeDetailsViewModel>()
            };

            var url = $"/postcodes";

            // var res = TestMethod();

            PostcodeBulkLookupResponse response = await _httpService.PostAsync<PostcodeBulkLookupResponse>(url, postcodes);

            await BuildPostcodesWithDetailsAsync(response, postcodesWithDetails);

            return postcodesWithDetails;
        }

        private async Task BuildPostcodesWithDetailsAsync(PostcodeBulkLookupResponse response,
            PostcodesListViewModel postcodesWithDetails)
        {
            if (response.Status == (int)HttpStatusCode.OK && response.Result != null)
            {
                foreach (var item in response.Result)
                {
                    var valid = await IsValidPostcode(item.Query);

                    if (valid)
                    {
                        postcodesWithDetails.Postcodes.Add(new PostcodeDetailsViewModel
                        {
                            Postcode = item.Result.Postcode,
                            Coordinates = new CoordinatesModel
                            {
                                Latitude = item.Result.Latitude,
                                Longitude = item.Result.Longitude
                            }
                        });
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(item.Query))
                        {
                            postcodesWithDetails.Postcodes.Add(new PostcodeDetailsViewModel
                            {
                                Postcode = item.Query + " " + "invalid"
                            });
                        }
                        else
                        {
                            postcodesWithDetails.Postcodes.Add(new PostcodeDetailsViewModel
                            {
                                Postcode = "Empty postcode"
                            });

                        }
                    }
                }
            }
            else
            {
                throw new ApiException();
            }
        }
    }
}