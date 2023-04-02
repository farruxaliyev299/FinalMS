using FinalMS.Gateway.DelegateHandlers;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<TokenExchangeDelegateHandler>();

builder.Configuration
    .AddJsonFile($"ocelot.{builder.Environment.EnvironmentName.ToLower()}.json")
    .AddEnvironmentVariables();

builder.Services.AddAuthentication().AddJwtBearer("GatewayAuthenticationScheme", options =>
{
    options.Authority = builder.Configuration["IdentityServerURL"];
    options.Audience = "resource_gateway";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddOcelot().AddDelegatingHandler<TokenExchangeDelegateHandler>();


var app = builder.Build();

await app.UseOcelot();

app.Run();
