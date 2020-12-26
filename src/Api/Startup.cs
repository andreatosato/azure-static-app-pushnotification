using Blazoring.PWA.API.Configurations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Blazoring.PWA.API.Startup))]
namespace Blazoring.PWA.API
{
    public class Startup : FunctionsStartup
    {
        private IFunctionsHostBuilder builder;
        private IConfiguration Configuration => builder.GetContext().Configuration;
        private IServiceCollection services => builder.Services;
        public override void Configure(IFunctionsHostBuilder builder)
        {
            this.builder = builder;
            services.Configure<WebPushNotificationConfig>(Configuration.GetSection(nameof(WebPushNotificationConfig)));
        }
    }
}
