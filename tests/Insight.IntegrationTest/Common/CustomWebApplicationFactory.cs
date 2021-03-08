using Insight.Application.Interfaces;
using Insight.Infrastructure.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Insight.IntegrationTest.Common
{
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Test");

            builder.ConfigureServices(services =>
            {

                // Build the service provider.
                var sp = services.BuildServiceProvider();
                services.AddScoped<IPostcodeService, PostcodesService>();

                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;

                scopedServices.GetRequiredService<ILogger<CustomWebApplicationFactory<TStartup>>>();

            });
        }
    }
}
