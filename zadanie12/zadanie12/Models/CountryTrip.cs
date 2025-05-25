using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie12.Models;

[Table("Country_Trip")]
public class CountryTrip
{
    [Key, Column(Order = 0)]
    public int IdCountry { get; set; }
        
    [Key, Column(Order = 1)]
    public int IdTrip { get; set; }
        
    [ForeignKey("IdCountry")]
    public virtual Country Country { get; set; }
        
    [ForeignKey("IdTrip")]
    public virtual Trip Trip { get; set; }
}