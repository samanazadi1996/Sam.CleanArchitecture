using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Sam.CleanArchitecture.WebApi.Infrastracture.Extensions
{
    public class ConfigureSwaggerOptions : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in provider.ApiVersionDescriptions)
            {
                var info = new OpenApiInfo()
                {
                    Title = $"{Assembly.GetCallingAssembly().GetName().Name} API",
                    Version = description.ApiVersion.ToString()
                };

                if (description.IsDeprecated) info.Description += " This API version has been deprecated.";

                options.SwaggerDoc(description.GroupName, info);
            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }
    }
}
