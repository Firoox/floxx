using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Floxx.Client.Extensions;
using Floxx.Client.Managers.Preferences;
using Floxx.Web.Client.Client.Settings;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Floxx.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args)
                                                .AddClientServices();
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            WebAssemblyHost host = builder.Build();
            //PreferenceManager storageService = host.Services.GetRequiredService<PreferenceManager>();
            
            //if (storageService != null)
            //{
            //    CultureInfo culture;
            //    Preference preference = await storageService.GetPreference();

            //    if (preference != null)
            //    {
            //        culture = new CultureInfo(preference.LanguageCode);
            //    }
            //    else
            //    {
            //        culture = new CultureInfo("de-DE");
            //    }

            //    CultureInfo.DefaultThreadCurrentCulture = culture;
            //    CultureInfo.DefaultThreadCurrentUICulture = culture;
            //}

            await builder.Build().RunAsync();
        }
    }
}
