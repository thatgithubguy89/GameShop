using GameShop.Api.Data;
using GameShop.Api.Extensions;
using GameShop.Api.Profiles;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
StripeConfiguration.ApiKey = builder.Configuration.GetValue<string>("Stripe:SecretKey");
var corsOrigins = builder.Configuration.GetSection("ValidOrigins").Get<string[]>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositoriesAndServices();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddDbContext<GameShopContext>(options =>
{
    options.UseSqlServer(builder.Configuration["GameShopConnection"]);
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["RedisUrl"];
});
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(options =>
    {
        options.WithOrigins(corsOrigins).AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors();
app.UseAuthorization();
app.MapControllers();
app.Run();
