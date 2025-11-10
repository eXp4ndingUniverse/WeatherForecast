using System.ComponentModel.DataAnnotations.Schema;
using WeatherForecast.DataAccess.Entities.Primitives;

namespace WeatherForecast.DataAccess.Entities;

[Table("WeatherData")]
public class WeatherDataEntity : BaseEntity
{
    public int? Temperature { get; set; }
    public int? AtmosphericPressure { get; set; }
    public int? AirHumidity { get; set; }
    public int? Visibility { get; set; }
    public int? UvIndex { get; set; }
    public int? WindSpeed { get; set; }
    
    public string? WindDirection { get; set; }
    
    public DateTime TimeData { get; set; }
    
    // Foreign keys
    public int CityId { get; set; }
    public int WeatherForecastId { get; set; }
    public CityEntity City { get; set; }
    public WeatherForecastEntity WeatherForecast { get; set; }
}