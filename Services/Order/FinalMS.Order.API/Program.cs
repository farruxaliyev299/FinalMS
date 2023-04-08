using FinalMS.Order.Application.Consumers;
using FinalMS.Order.Infrastructure;
using FinalMS.Shared.Services;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit((x =>
{
    x.AddConsumer<CreateOrderMessageCommandConsumer>();
    x.AddConsumer<ProductNameChangedEventConsumer>();

    x.UsingRabbitMq((context, config) =>
    {
        config.Host(builder.Configuration.GetSection("RabbitMQUrl").Value, @"/", host =>
        {
            host.Username("guest");
            host.Password("guest");
        });

        config.ReceiveEndpoint("create-order", ep =>
        {
            ep.ConfigureConsumer<CreateOrderMessageCommandConsumer>(context);
        });
        config.ReceiveEndpoint("product-name-changed-event", ep =>
        {
            ep.ConfigureConsumer<ProductNameChangedEventConsumer>(context);
        });
    });
}));

builder.Services.AddMassTransitHostedService();

#region CustomServices
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ISharedIdentityService, SharedIdentityService>();
#endregion

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(FinalMS.Order.Application.Queries.GetOrdersByUserIdQuery.GetOrdersByUserIdQueryHandler).Assembly);
});

var requireAuthorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.Authority = builder.Configuration.GetSection("IdentityServerURL").Value;
    options.Audience = "resource_order";
    options.RequireHttpsMetadata = false;
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthorizePolicy));
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql"), config =>
    {
        config.MigrationsAssembly("FinalMS.Order.Infrastructure");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
