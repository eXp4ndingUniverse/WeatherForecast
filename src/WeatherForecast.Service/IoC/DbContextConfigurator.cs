using Microsoft.EntityFrameworkCore;
using WeatherForecast.DataAccess;
using WeatherForecast.Settings;

namespace WeatherForecast.IoC;

public class DbContextConfigurator
{
    public static void ConfigureService(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("WeatherForecastDbContext");
        
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string 'WeatherForecastDbContext' not found in configuration");
        }
        
        services.AddDbContextFactory<WeatherForecastDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        }, ServiceLifetime.Scoped);

    }

    public static void ConfigureApplication(IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var contextFactory = scope.ServiceProvider.GetRequiredService<IDbContextFactory<WeatherForecastDbContext>>();
        using var context = contextFactory.CreateDbContext();
        context.Database.Migrate();
    }
}