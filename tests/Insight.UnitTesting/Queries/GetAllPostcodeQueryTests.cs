using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Insight.Application.Common.Exceptions;
using Insight.Application.PostCodesFeatures.Queries;
using Insight.Domain;
using Insight.UnitTesting.Common;
using Shouldly;
using Xunit;

namespace Insight.UnitTesting.Queries
{
    public class GetAllPostcodeQueryTests : QueryTestBase
    {
        private readonly GetAllPostcodeQuery.GetAllPostcodeQueryHandler _queryHandler;
        public GetAllPostcodeQueryTests()
        {
            _queryHandler = new GetAllPostcodeQuery.GetAllPostcodeQueryHandler(MockPostcodeRepo, _mapper);
        }


        [Fact]
        public async Task GetPostcodesList()
        {
            //Arrange
            var request = new GetAllPostcodeQuery { PostCodes = new[] { "GU1 1AA", "GU1 1AD" } };
            var handler = new GetAllPostcodeQuery.GetAllPostcodeQueryHandler(MockPostcodeRepo, _mapper);
            int minimumTestPostcodeCount = 1;

            //Act
            var result = await handler.Handle(request, CancellationToken.None);

            //Assert
            result.ShouldBeOfType<PostcodesListViewModel>();
            result.Postcodes.Count.ShouldBeGreaterThan(minimumTestPostcodeCount);
        }

        [Fact]
        public async Task Handle_ThrowsInValidPostcodeException_If_ThePostcode_PassedIn_Null()
        {
            var request = new GetAllPostcodeQuery()
            {
                PostCodes = null
            };

            var s = await Assert.ThrowsAsync<PostcodeException>(() => _queryHandler.Handle(request, CancellationToken.None));

            Assert.Equal(Constants.UnprocessableEntity, s.Error);
            Assert.Equal((int)HttpStatusCode.UnprocessableEntity, s.Status);
            Assert.Equal(Constants.AListWithAtLeastOnePostcodeIsMandatory, s.Message);
        }
    }
}
