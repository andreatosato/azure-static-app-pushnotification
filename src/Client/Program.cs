using Blazor.IndexedDB.Framework;
using Blazoring.PWA.Client.Services;
using Blazorise;
using Blazorise.Icons.FontAwesome;
using Blazorise.Icons.Material;
using Blazorise.Material;
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

            builder.Services.AddBlazorise(options =>
            {
                options.ChangeTextOnKeyPress = true;
            })
            .AddMaterialProviders()
            .AddMaterialIcons();
            /*.AddFontAwesomeIcons()*/;

#if DEBUG
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("http://localhost:7071/api/") });
#else
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri($"{builder.HostEnvironment.BaseAddress}/api/") });
#endif
            builder.Services.AddTransient<ISnackbarService, SnackbarService>();
            builder.Services.AddTransient<IUserService, UserService>();
            builder.Services.AddScoped<IIndexedDbFactory, IndexedDbFactory>();
            builder.Services.AddTransient<JSInstanceHelper>();

            var host = builder.Build();
            host.Services
              .UseMaterialProviders()
              .UseMaterialIcons();
            //.UseFontAwesomeIcons();

            JSHelper.Client = host.Services.GetRequiredService<HttpClient>();

            await host.RunAsync();
        }
    }
}
