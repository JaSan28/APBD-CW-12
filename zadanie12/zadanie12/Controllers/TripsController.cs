using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using zadanie12.DTOs;
using zadanie12.Models;
using zadanie12.Services;

namespace zadanie12.Controllers;

[ApiController]
[Route("api/trips")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;
    private readonly AppDbContext _context;

    public TripsController(ITripService tripService, AppDbContext context)
    {
        _tripService = tripService;
        _context = context; 
    }
    

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var trips = await _tripService.GetTripsAsync(page, pageSize);
        var totalTrips = await _context.Trips.CountAsync();
        var totalPages = (int)Math.Ceiling(totalTrips / (double)pageSize);
    
        return Ok(new 
        { 
            pageSize, 
            allPages = totalPages, 
            trips 
        });
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClient(int idTrip, [FromBody] AssignClientRequest request)
    {
        var result = await _tripService.AssignClientToTripAsync(idTrip, request);
        
        if (!result) return BadRequest("Nie można przypisać klienta do wycieczki");
        return Ok();
    }
}