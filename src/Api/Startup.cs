using Blazoring.PWA.API.Configurations;
using Blazoring.PWA.API.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

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
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            services.AddTransient<ISeedGeneratorService, BogusGeneratorService>();
            builder.Services
                .AddOptions<WebPushNotificationConfig>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("WebPushNotification").Bind(settings);
                });
        }
    }
}
