using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie12.Models;

[Table("Trip")]
public class Trip
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdTrip { get; set; }
        
    [Required]
    [MaxLength(120)]
    public string Name { get; set; }
        
    [Required]
    [MaxLength(220)]
    public string Description { get; set; }
        
    [Required]
    public DateTime DateFrom { get; set; }
        
    [Required]
    public DateTime DateTo { get; set; }
        
    [Required]
    public int MaxPeople { get; set; }
        
    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
    public virtual ICollection<CountryTrip> CountryTrips { get; set; } = new List<CountryTrip>();
}