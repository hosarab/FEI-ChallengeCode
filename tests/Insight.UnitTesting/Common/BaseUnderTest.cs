using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Insight.Application.Interfaces;
using Insight.Application.Wrappers;
using Insight.Infrastructure.Services;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace Insight.UnitTesting.Common
{
    public class BaseUnderTest
    {
        public IHttpService HttpClientService { get; set; }
        public ICacheService HttpCacheService { get; set; }

        const string BaseUrl = "https://postcodes.io/";
        public BaseUnderTest()
        {
            var memCache = new Mock<IMemoryCache>();
            var entryMock = new Mock<ICacheEntry>();

            memCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(entryMock.Object);

            HttpCacheService = new CacheService(memCache.Object);


            var httpResponse = new HttpResponseMessage
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(
                    JsonConvert.SerializeObject(new PostcodeBulkLookupResponse(), Formatting.Indented), Encoding.UTF8,
                    "application/json")
            };

            var mockHandler = new Mock<HttpMessageHandler>();
            mockHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync",
                    ItExpr.Is<HttpRequestMessage>(r => r.Method ==
                        HttpMethod.Post && r.RequestUri.ToString().StartsWith(BaseUrl)),
                    ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(mockHandler.Object)
            {
                BaseAddress = new Uri(BaseUrl)

            };

            HttpClientService = new HttpService(httpClient);
        }
    }
}
