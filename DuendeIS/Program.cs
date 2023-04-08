using Duende;
using Duende.IdentityServer.Validation;
using FinalMS.DuendeIS;
using FinalMS.DuendeIS.Data;
using FinalMS.DuendeIS.Initializer;
using FinalMS.DuendeIS.Models;
using FinalMS.DuendeIS.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IDbInitializer, DbInitializer>();


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
    options.IssuerUri = builder.Configuration.GetSection("IssuerURI").Value;
}).AddInMemoryIdentityResources(SeedData.IdentityResources)
  .AddInMemoryApiResources(SeedData.ApiResources)
  .AddInMemoryApiScopes(SeedData.ApiScopes)
  .AddInMemoryClients(SeedData.Clients)
  .AddAspNetIdentity<ApplicationUser>()
  .AddResourceOwnerValidator<IdentityResourceOwnerPasswordValidator>()
  .AddExtensionGrantValidator<TokenExchangeExtensionGrantValidator>();

builder.Services.AddLocalApiAuthentication();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseIdentityServer();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
    dbInitializer.Initialize();
}
app.MapControllerRoute(
name: "default",
pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
