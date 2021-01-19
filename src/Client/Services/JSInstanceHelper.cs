
using Microsoft.JSInterop;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Blazoring
{
    public class JSInstanceHelper
    {
        [JSInvokable("LogText")]
        public string LogText(string text)
        {
            Console.WriteLine(text);
            return DateTime.Now.ToString();
        }
    }

    public static class JSHelper
    {
        [JSInvokable("GetUserInfo")]
        public static async Task<string> GetUserInfo(string value)
        {
#if DEBUG
            return "Andrea";
#else
            var profile = await Client.GetStringAsync("/.auth/me");
            return profile;
#endif
        }

        public static HttpClient Client { get; set; }

        // https://docs.microsoft.com/it-it/aspnet/core/blazor/call-dotnet-from-javascript?view=aspnetcore-5.0
        // https://github.com/dotnet/AspNetCore.Docs/blob/master/aspnetcore/blazor/common/samples/3.x/BlazorWebAssemblySample/wwwroot/exampleJsInterop.js
    }
}

