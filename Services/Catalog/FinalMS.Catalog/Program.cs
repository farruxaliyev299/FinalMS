using FinalMS.Catalog.Services.Categories;
using FinalMS.Catalog.Services.Products;
using FinalMS.Catalog.Services.Stores;
using FinalMS.Catalog.Settings;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder(args);

IdentityModelEventSource.ShowPII = true;

builder.Services.AddMassTransit((x =>
{
    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetSection("RabbitMQUrl").Value, @"/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });
    });
}));

builder.Services.AddMassTransitHostedService();

#region CustomServices
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IStoreService, StoreService>();
#endregion

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program));

#region DatabaseSettings
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.AddSingleton<IDatabaseSettings>(sp =>
{
    return sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
});
#endregion

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority =  builder.Configuration.GetSection("IdentityServerURL").Value;
    options.Audience = "resource_catalog";
    options.RequireHttpsMetadata = false;
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
