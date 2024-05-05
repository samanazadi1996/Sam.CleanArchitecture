using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorWasmPwa
    {
    public class Program
        {
        public static async Task Main(string[] args)
            {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            
            builder.Services.AddOidcAuthentication(options =>
            {
                //https://learn.microsoft.com/en-us/aspnet/core/blazor/security/webassembly/standalone-with-authentication-library?view=aspnetcore-8.0&tabs=visual-studio

                // Configure your authentication provider options here.
                // For more information, see https://aka.ms/blazor-standalone-auth
                builder.Configuration.Bind("Local", options.ProviderOptions);
            });

            /* Configure Facebook authentication
           builder.Services.AddOidcAuthentication(options =>
           {
               options.ProviderOptions.Authority = "https://www.facebook.com/";
               options.ProviderOptions.ClientId = "YOUR_FACEBOOK_APP_ID"; // Replace with your actual Facebook app ID
               options.ProviderOptions.RedirectUri = "https://localhost:5001/authentication/login-callback";
               // Other options (scopes, post-logout redirect, etc.)
           });
           */
            await builder.Build().RunAsync();
            }
        }
    }
