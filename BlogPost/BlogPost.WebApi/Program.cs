using AspNetCoreRateLimit;
using BlogPost.WebApi.Authentication;
using StackExchange.Redis;

var PresentationAssembly = typeof(Presentation.Controllers.BlogPostController).Assembly;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddMemoryCache();
builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>
            {
                new RateLimitRule
                {
                    Endpoint = "*",
                    Limit = 100, // Number of requests
                    Period = "1m" // Time period (1 minute)
                }
            };
});
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.ConfigurationOptions = new ConfigurationOptions
    {
        //silently retry in the background if the Redis connection is temporarily down
        AbortOnConnectFail = false
        
    };
    options.Configuration = "localhost:6379";
    options.InstanceName = "AspNetRateLimit";
});
builder.Services.AddSingleton<IMockAuthenticationService, MockAuthenticationService>();
builder.Services.AddControllers().AddApplicationPart(PresentationAssembly);
builder.Services.AddSingleton<IClientPolicyStore, DistributedCacheClientPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}


app.UseHttpsRedirection();
app.UseIpRateLimiting();
app.UseAuthentication();
//app.UseAuthorization();
//var summaries = new[]
//{
//    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
//};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateTime.Now.AddDays(index),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast");


//internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}