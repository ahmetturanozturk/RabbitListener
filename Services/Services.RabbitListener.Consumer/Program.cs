using RabbitListener.Shared.Services;
using Services.RabbitListener.Publisher.Services;
using Services.RabbitListener.RabbitMQ.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IRabbitMQService, RabbitMQService>();
builder.Services.AddSingleton<IConsumerService, ConsumerService>();
builder.Services.AddSingleton<ILoggingData, LoggingData>();

var app = builder.Build();

var logger =
app.Services
        .GetRequiredService<ILoggerFactory>()
        .CreateLogger<Program>();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
}

app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
