using WeatherForecast.Settings;
using WeatherForecast.IoC;

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
    .Build();

var settings = WeatherForecastSettingsReader.Read(configuration);
var builder = WebApplication.CreateBuilder(args);

DbContextConfigurator.ConfigureService(builder.Services, builder.Configuration);
SerilogConfigurator.ConfigureService(builder);
SwaggerConfigurator.ConfigureServices(builder.Services);

var app = builder.Build();

DbContextConfigurator.ConfigureApplication(app);
SerilogConfigurator.ConfigureApplication(app);
SwaggerConfigurator.ConfigureApplication(app);

app.UseHttpsRedirection();

app.MapGet("/", () => "РАБОТАЕТ");

app.Run();