using System.Threading.Tasks;
using Insight.Application.Interfaces;
using Insight.Infrastructure.Services;
using Moq;
using Xunit;

namespace Insight.UnitTesting
{
    public class ValidateTests
    {
        private readonly PostcodesService _sut;

        public ValidateTests()
        {
            var cacheSrv = new Mock<ICacheService>();
            var httpService = new Mock<IHttpService>();
            _sut = new PostcodesService(httpService.Object, cacheSrv.Object);
        }

        [Fact]
        public async void Validate_returns_true_for_valid_postcode()
        {
            var result = await _sut.ValidateAsync("GU1 1AA");
            Assert.True(result);
        }

        [Fact]
        public async void Validate_returns_false_for_nonsense_postcode()
        {
            var result = await _sut.ValidateAsync("FAKE_POSTCODE");
            Assert.False(result);
        }

        [Fact]
        public async Task Validate_returns_true_for_valid_postcode_async()
        {
            var result = await _sut.ValidateAsync("GU1 1AD");
            Assert.True(result);
        }
    }
}
