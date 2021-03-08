using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Insight.Application.Interfaces;
using Insight.Application.PostCodesFeatures.Queries;
using Moq;
using Xunit;

namespace Insight.UnitTesting.Services
{
    public class PostcodeServiceTests
    {
        private readonly Mock<IPostcodeService> _sut;

        public PostcodeServiceTests()
        {
            _sut = new Mock<IPostcodeService>();
        }

        [Theory]
        [InlineData("KT25BG")]
        public async void Test1(string postcode)
        {
            var expectedPostcode = postcode;
            var model = new PostcodeDetailsViewModel() { Postcode = postcode };

            _sut.Setup(x => x.LookupByPostCode(It.IsAny<string>())).Returns(Task.FromResult(model));

            var result = await _sut.Object.LookupByPostCode(model.Postcode);

            Assert.True(result.Postcode != null);
            Assert.Equal(expectedPostcode, result.Postcode);
        }

        [Theory]
        [InlineData("KT25BG")]
        public async void Should_Find_Valid_Postcode(string postcode)
        {
            var expectedPostcode = postcode;
            var model = new PostcodeDetailsViewModel() { Postcode = postcode };

            _sut.Setup(x => x.LookupByPostCode(It.IsAny<string>())).Returns(Task.FromResult(model));

            var result = await _sut.Object.LookupByPostCode(postcode);
            Assert.Equal(expectedPostcode, result.Postcode);

        }

        [Theory]
        [InlineData("KT25BG")]
        public async void Should_Verify_Invoke_GivenMethod_Test(string postcode)
        {
            var model = new PostcodeDetailsViewModel() { Postcode = postcode };

            _sut.Setup(x => x.LookupByPostCode(It.IsAny<string>())).Returns(Task.FromResult(model));

            await _sut.Object.LookupByPostCode(model.Postcode);
            _sut.Verify(x => x.LookupByPostCode(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async void Should_Be_Return_When_BulkLookup_Method_IsCalled()
        {
            string[] data = { "EX11NT", "KT25BG" };
            IEnumerable<string> postcodes = data.ToArray();

            //mock the method so when it is called, we handle it
            _sut.Setup(s =>
                s.BulkLookup(It.IsAny<IEnumerable<string>>()))
                .Returns(Task.FromResult(new PostcodesListViewModel()));

            await _sut.Object.BulkLookup(postcodes);

            _sut.Verify(x => x.BulkLookup(It.IsAny<IEnumerable<string>>()), Times.Once);

            _sut.Verify(x => x.BulkLookup(data), Times.Once);

        }

        private static void TestResults(PostcodesListViewModel result)
        {
            Assert.Equal(2, result.Postcodes.Count);
            Assert.Contains(result.Postcodes, x => x.Postcode == "EX1 1NT");
            Assert.Contains(result.Postcodes, x => x.Postcode == "KT2 5BG");

        }
    }
}
