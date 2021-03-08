using System.Net.Http;
using System.Threading.Tasks;
using Insight.Application.PostCodesFeatures.Queries;
using Insight.IntegrationTest.Common;
using Insight.WebApi;
using Newtonsoft.Json;
using Shouldly;
using Xunit;

namespace Insight.IntegrationTest.Controllers
{
    public class PostcodesControllerTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        public CustomWebApplicationFactory<Startup> Factory { get; }
        private readonly HttpClient _client;

        public PostcodesControllerTests(CustomWebApplicationFactory<Startup> factory)
        {
            Factory = factory;
            Factory = factory;
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData("/api/Postcodes?postcode=kt25bg")]
        public async Task GetApisRouteTest(string url)
        {
            // Act
            var response = await _client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            if (response.Content.Headers.ContentType != null)
                Assert.Equal("application/json; charset=utf-8",
                    response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Create_GivenSendPostcodeCommand_ReturnsSuccessStatusCode()
        {
            // Act
            GetAllPostcodeQuery command = new GetAllPostcodeQuery
            {
                PostCodes = new[] { "GU1 1AA", "GU1 1AD" }
            };

            // Arrange
            var content = Utilities.GetRequestContent(command);
            var response = await _client.PostAsync("/api/postcodes", content);
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<PostcodesListViewModel>(stringResponse);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Contains(result.Postcodes, x => x.Postcode == "GU1 1AA");
            Assert.Contains(result.Postcodes, x => x.Postcode == "GU1 1AD");
        }

        [Fact]
        public async Task GetAll_ReturnsPostcodeListViewModel()
        {
            // Act
            GetAllPostcodeQuery command = new GetAllPostcodeQuery
            {
                PostCodes = new[] { "GU1 1AA", "GU1 1AD" }
            };
            var content = Utilities.GetRequestContent(command);
            var response = await _client.PostAsync("/api/postcodes", content);
            var stringResponse = await response.Content.ReadAsStringAsync();

            // Arrange
            response.EnsureSuccessStatusCode();
            var vm = await Utilities.GetResponseContent<PostcodesListViewModel>(response);

            // Assert
            vm.ShouldBeOfType<PostcodesListViewModel>();
            vm.Postcodes.ShouldNotBeEmpty();
            vm.Postcodes.Count.ShouldBeEquivalentTo(2);
        }
    }
}
