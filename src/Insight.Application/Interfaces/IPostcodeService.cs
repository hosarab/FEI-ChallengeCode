using System.Collections.Generic;
using System.Threading.Tasks;
using Insight.Application.PostCodesFeatures.Queries;

namespace Insight.Application.Interfaces
{
    public interface IPostcodeService
    {
        Task<PostcodeDetailsViewModel> LookupByPostCode(string postcode);
        Task<bool> ValidateAsync(string postcode);
        Task<PostcodesListViewModel> BulkLookup(IEnumerable<string> postcodes);
    }
}