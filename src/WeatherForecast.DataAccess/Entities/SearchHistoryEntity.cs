using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.DataAccess.Entities;

[Table("SearchHistory")]
public class SearchHistoryEntity : BaseEntity
{
    public DateTime TimeSearch { get; set; }
    
    // Foreign keys
    public int UserId { get; set; }
    public int CityId { get; set; }
    
    public UserEntity User { get; set; }
    public CityEntity City { get; set; }
}