using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie12.Models;

[Table("Client")]
public class Client
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public required int IdClient { get; set; }
        
    [Required]
    [MaxLength(120)]
    public required string FirstName { get; set; }
        
    [Required]
    [MaxLength(120)]
    public required string LastName { get; set; }
        
    [Required]
    [MaxLength(120)]
    public required string Email { get; set; }
        
    [Required]
    [MaxLength(120)]
    public required string Telephone { get; set; }
        
    [Required]
    [MaxLength(120)]
    public required string Pesel { get; set; }

    public virtual ICollection<ClientTrip> ClientTrips { get; set; } = new List<ClientTrip>();
}