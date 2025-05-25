using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace zadanie12.Models;

[Table("Client_Trip")]
public class ClientTrip
{
    [Key, Column(Order = 0)]
    public int IdClient { get; set; }
        
    [Key, Column(Order = 1)]
    public int IdTrip { get; set; }
        
    [Required]
    public DateTime RegisteredAt { get; set; }
    public DateTime? PaymentDate { get; set; }
        
    [ForeignKey("IdClient")]
    public virtual Client Client { get; set; } = null!;
        
    [ForeignKey("IdTrip")]
    public virtual Trip Trip { get; set; } = null!;
}