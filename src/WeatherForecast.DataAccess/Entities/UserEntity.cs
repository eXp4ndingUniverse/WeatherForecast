using System.ComponentModel.DataAnnotations.Schema;
using WeatherForecast.DataAccess.Entities.Primitives;

namespace WeatherForecast.DataAccess.Entities;

[Table("Users")]
public class UserEntity : BaseEntity
{
    public string Username { get; set; }

    public string Email { get; set; }

    public DateTime RegistrationDate { get; set; }
    
    public Role Role { get; set; }
    public ICollection<FavoriteCityEntity> FavoriteCities { get; set; }
    public ICollection<SearchHistoryEntity> SearchHistories { get; set; }
}