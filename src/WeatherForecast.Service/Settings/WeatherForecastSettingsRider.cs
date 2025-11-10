namespace WeatherForecast.Settings;

public static class WeatherForecastSettingsReader
{
    public static WeatherForecastSettings Read(IConfiguration configuration)
    {
        return new WeatherForecastSettings()
        {
            WeatherForecastDbConnectionString = configuration.GetConnectionString("WeatherForecastDbContext")
                                                ?? configuration["WeatherForecastDbContextConnectionString"]
                                                ?? throw new Exception("Connection string not found")
        };
    }
}