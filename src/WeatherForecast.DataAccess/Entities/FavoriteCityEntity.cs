using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.DataAccess.Entities;

[Table("FavoriteCities")]
public class FavoriteCityEntity : BaseEntity
{
    // Foreign keys
    public int UserId { get; set; }
    public int CityId { get; set; }
    
    public UserEntity User { get; set; }
    public CityEntity City { get; set; }
}