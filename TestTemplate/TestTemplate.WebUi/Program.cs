using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Reflection;
using TestTemplate.Application;
using TestTemplate.Application.Interfaces;
using TestTemplate.Infrastructure.FileManager;
using TestTemplate.Infrastructure.FileManager.Contexts;
using TestTemplate.Infrastructure.Identity;
using TestTemplate.Infrastructure.Identity.Contexts;
using TestTemplate.Infrastructure.Identity.Models;
using TestTemplate.Infrastructure.Identity.Seeds;
using TestTemplate.Infrastructure.Persistence;
using TestTemplate.Infrastructure.Persistence.Contexts;
using TestTemplate.Infrastructure.Resources;
using TestTemplate.WebUi.Infrastracture.Extensions;
using TestTemplate.WebUi.Infrastracture.Middlewares;
using TestTemplate.WebUi.Infrastracture.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationLayer();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);
builder.Services.AddFileManagerInfrastructure(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddResourcesInfrastructure(builder.Configuration);

builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddIdentityCookie(builder.Configuration);

builder.Services.AddControllersWithViews().AddFluentValidation(options =>
{
    options.ImplicitlyValidateChildProperties = true;
    options.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly());
});

builder.Services.AddCustomLocalization(builder.Configuration);

builder.Services.AddScoped<IAuthenticatedUserService, AuthenticatedUserService>();
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await services.GetRequiredService<IdentityContext>().Database.MigrateAsync();
    await services.GetRequiredService<ApplicationDbContext>().Database.MigrateAsync();
    await services.GetRequiredService<FileManagerDbContext>().Database.MigrateAsync();

    //Seed Data
    await DefaultRoles.SeedAsync(services.GetRequiredService<RoleManager<ApplicationRole>>());
    await DefaultBasicUser.SeedAsync(services.GetRequiredService<UserManager<ApplicationUser>>());
}
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseCustomLocalization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
