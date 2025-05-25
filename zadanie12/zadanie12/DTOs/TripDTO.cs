namespace zadanie12.DTOs;

public class TripDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public required List<CountryDTO> Countries { get; set; }
    public required List<ClientDTO> Clients { get; set; }
}