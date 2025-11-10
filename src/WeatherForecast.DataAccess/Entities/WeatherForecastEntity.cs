using System.ComponentModel.DataAnnotations.Schema;
using WeatherForecast.DataAccess.Entities.Primitives;

namespace WeatherForecast.DataAccess.Entities;

[Table("WeatherForecast")]
public class WeatherForecastEntity : BaseEntity
{
    public DateTime ForecastDay { get; set; }
    public int? MaxTemp { get; set; }
    public int? MinTemp { get; set; }
    public int? DayTemp { get; set; }
    public int? NightTemp { get; set; }
    public int? AtmosphericPressure { get; set; }
    public int? AirHumidity { get; set; }
    
    public string? TypeWeather { get; set; }
    
    // Foreign key
    public int CityId { get; set; }
    
    public CityEntity City { get; set; }
    public ICollection<WeatherDataEntity> WeatherData { get; set; }
}