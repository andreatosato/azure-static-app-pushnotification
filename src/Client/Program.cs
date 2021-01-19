using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazoring.PWA.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");


#if DEBUG
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7071/api/") });
#else
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}/api/") });
#endif
            await builder.Build().RunAsync();
        }
    }
}
