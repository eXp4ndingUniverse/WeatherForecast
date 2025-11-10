using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeatherForecast.DataAccess.Entities;

[Table("Countries")]
public class CountryEntity : BaseEntity
{
    public string CountryName { get; set; }
    
    public ICollection<CityEntity> Cities { get; set; }
}