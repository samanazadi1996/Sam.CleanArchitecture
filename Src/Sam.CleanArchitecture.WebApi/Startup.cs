using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sam.CleanArchitecture.Application;
using Sam.CleanArchitecture.Application.Interfaces;
using Sam.CleanArchitecture.Infrastructure.FileManager;
using Sam.CleanArchitecture.Infrastructure.Identity;
using Sam.CleanArchitecture.Infrastructure.Persistence;
using Sam.CleanArchitecture.Infrastructure.Resources;
using Sam.CleanArchitecture.WebApi.Infrastracture.Extensions;
using Sam.CleanArchitecture.WebApi.Infrastracture.Middlewares;
using Sam.CleanArchitecture.WebApi.Infrastracture.Services;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace Sam.CleanArchitecture.WebApi
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Sam.CleanArchitecture.WebApi v1"));
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
