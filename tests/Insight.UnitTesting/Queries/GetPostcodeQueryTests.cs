using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Insight.Application.Common.Exceptions;
using Insight.Application.PostCodesFeatures.Queries;
using Insight.Domain;
using Insight.UnitTesting.Common;
using Xunit;

namespace Insight.UnitTesting.Queries
{
    public class GetPostcodeQueryTests : QueryTestBase
    {
        private readonly GetPostcodeQuery.GetPostcodeQueryHandler _queryHandler;

        public GetPostcodeQueryTests()
        {
            _queryHandler = new GetPostcodeQuery.GetPostcodeQueryHandler(MockPostcodeRepo, _mapper);
        }

        [Fact]
        public async Task Handle_WhenTryingToGetDetailsOfNotExistingPostcode_ThrowsInValidPostcodeException()
        {
            var request = new GetPostcodeQuery()
            {
                PostCode = string.Empty
            };

            await Assert.ThrowsAsync<PostcodeException>(() => _queryHandler.Handle(request, CancellationToken.None));
        }

        [Theory]
        [InlineData("GU1")]
        [InlineData("??GU1")]

        public async Task Handle_ThrowsInValidPostcodeException_If_ThePostcode_PassedIn_Is_InvalidOne(string postcode)
        {
            var request = new GetPostcodeQuery()
            {
                PostCode = postcode
            };

            var s = await Assert.ThrowsAsync<PostcodeException>(() => _queryHandler.Handle(request, CancellationToken.None));

            Assert.Equal(Constants.NotFound, s.Error);
            Assert.Equal((int)HttpStatusCode.NotFound, s.Status);
            Assert.Equal(Constants.YouHaveEnteredAnInvalidPostcode, s.Message);
        }

        [Fact]
        public async Task Handle_ThrowsInValidPostcodeException_If_ThePostcode_PassedIn_Null()
        {
            var request = new GetPostcodeQuery()
            {
                PostCode = null
            };

            var s = await Assert.ThrowsAsync<PostcodeException>(() => _queryHandler.Handle(request, CancellationToken.None));

            Assert.Equal(Constants.UnprocessableEntity, s.Error);
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, s.Status);
            Assert.Equal(Constants.ThePostcodeIsMandatory, s.Message);
        }
    }
}
