using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace FlappyBird.Blazor
{
    /// <summary>
    /// Von der Laufzeitumgebung generierte Klasse
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Von der Laufzeitumgebung generierte Methode, welche beim Start des Programms
        /// als erstes aufgerufen wird.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
