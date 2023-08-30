using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using TestTemplate.Application;
using TestTemplate.Application.Interfaces;
using TestTemplate.Infrastructure.FileManager;
using TestTemplate.Infrastructure.Identity;
using TestTemplate.Infrastructure.Persistence;
using TestTemplate.Infrastructure.Resources;
using TestTemplate.WebApi.Infrastracture.Extensions;
using TestTemplate.WebApi.Infrastracture.Middlewares;
using TestTemplate.WebApi.Infrastracture.Services;

namespace TestTemplate.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationLayer(Configuration);
            services.AddPersistenceInfrastructure(Configuration);
            services.AddFileManagerInfrastructure(Configuration);
            services.AddIdentityInfrastructure(Configuration);
            services.AddResourcesInfrastructure(Configuration);

            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
            services.AddDistributedMemoryCache();
            services.AddJwt(Configuration);

            services.AddControllers().AddFluentValidation(options =>
            {
                options.ImplicitlyValidateChildProperties = true;

                options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            });
            services.AddSwaggerWithVersioning();
            services.AddCors(x =>
            {
                x.AddPolicy("Any", b =>
                {
                    b.AllowAnyOrigin();
                    b.AllowAnyHeader();
                    b.AllowAnyMethod();

                });
            });

            services.AddHealthChecks();
            services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TestTemplate.WebApi v1"));
            }
            app.UseCors("Any");
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwaggerWithVersioning();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseHealthChecks("/health");

            var locale = "en";
            var localizationOptions = new RequestLocalizationOptions
            {
                SupportedCultures = new List<CultureInfo> { new CultureInfo(locale) },
                SupportedUICultures = new List<CultureInfo> { new CultureInfo(locale) },
                DefaultRequestCulture = new RequestCulture(locale)
            };

            app.UseRequestLocalization(localizationOptions);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
