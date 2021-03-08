using AutoMapper;
using Insight.Application.Interfaces;
using Insight.Application.Mappings;
using Insight.Infrastructure.Services;

namespace Insight.UnitTesting.Common
{
    public class QueryTestBase : BaseUnderTest
    {
        protected IPostcodeService MockPostcodeRepo { get; }
        protected readonly IMapper _mapper;

        public QueryTestBase()
        {

            MockPostcodeRepo = new PostcodesService(HttpClientService, HttpCacheService);
            var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneralProfile>();
            });

            _mapper = configurationProvider.CreateMapper();
        }
    }
}