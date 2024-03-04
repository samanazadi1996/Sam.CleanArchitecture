using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CleanArchitecture.WebApi.Infrastracture.Extensions
{
    public static class LocalizationExtentions
    {
        public static IServiceCollection AddCustomLocalization(this IServiceCollection services, IConfiguration configuration)
        {
            var supportedCultures = configuration.GetSection("Localization:SupportedCultures")
                .Get<List<string>>().Select(p => new CultureInfo(p)).ToArray();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(configuration["Localization:DefaultRequestCulture"]);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });

            return services;
        }
        public static IApplicationBuilder UseCustomLocalization(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(app.ApplicationServices.GetRequiredService<IOptions<RequestLocalizationOptions>>().Value);

            return app;
        }

    }

}
