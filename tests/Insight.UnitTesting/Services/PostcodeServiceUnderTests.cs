using System.Threading.Tasks;
using Insight.Infrastructure.Services;
using Insight.UnitTesting.Common;
using Xunit;

namespace Insight.UnitTesting.Services
{
    public class PostcodeServiceUnderTests : BaseUnderTest
    {
        private readonly PostcodesService _sut;

        public PostcodeServiceUnderTests()
        {
            _sut = new PostcodesService(HttpClientService, HttpCacheService);
        }

        [Fact]
        public async Task LookUp_Invalid_Postcodes_results_async()
        {
            var result = await _sut.BulkLookup(new[] { "GU1 1AB" });

            Assert.Contains(result.Postcodes, x => x.Postcode == "GU1 1AB invalid");
            Assert.Contains(result.Postcodes, x => x.Coordinates == null);
        }


        [Fact]
        public async Task PostCode_Valid_Details_results_async()
        {
            var longitude = -0.582332;
            var latitude = 51.245283;

            var result = await _sut.LookupByPostCode("GU1 1AA");

            Assert.Equal(latitude, result.Coordinates.Latitude);
            Assert.Equal(longitude, result.Coordinates.Longitude);
            Assert.Equal("GU1 1AA", result.Postcode);
        }


        [Fact]
        public async Task BulkLookup_Valid_Postcodes_results_async()
        {

            var result = await _sut.BulkLookup(new[] { "GU1 1AA", "GU1 1AD" });

            Assert.Equal(2, result.Postcodes.Count);
            Assert.Contains(result.Postcodes, x => x.Postcode == "GU1 1AA");
            Assert.Contains(result.Postcodes, x => x.Postcode == "GU1 1AD");

        }
    }
}
