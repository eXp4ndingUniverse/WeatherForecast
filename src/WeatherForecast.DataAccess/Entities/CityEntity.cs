using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.DataAccess.Entities;

[Table("Cities")] 
public class CityEntity : BaseEntity
{
    public string? CityName { get; set; }
    
    public string LatitudeLongitude { get; set; }
    
    // Foreign key
    public int CountryId { get; set; }
    
    public CountryEntity Country { get; set; }
    public ICollection<WeatherForecastEntity> WeatherForecasts { get; set; }
    public ICollection<WeatherDataEntity> WeatherData { get; set; }
    public ICollection<FavoriteCityEntity> FavoriteCities { get; set; }
    public ICollection<SearchHistoryEntity> SearchHistories { get; set; }
}