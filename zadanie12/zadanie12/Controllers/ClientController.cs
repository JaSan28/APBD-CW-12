using Microsoft.AspNetCore.Mvc;
using zadanie12.Services;

namespace zadanie12.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly ITripService _tripService;

    public ClientsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var result = await _tripService.DeleteClientAsync(idClient);
        
        if (!result) return BadRequest("Nie można usunąć klienta - ma przypisane wycieczki lub nie istnieje");
        return NoContent();
    }
}