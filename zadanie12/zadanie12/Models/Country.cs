using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie12.Models;

[Table("Country")]
public class Country
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCountry { get; set; }
        
    [Required]
    [MaxLength(120)]
    public string Name { get; set; }
        
    public virtual ICollection<CountryTrip> CountryTrips { get; set; }
}