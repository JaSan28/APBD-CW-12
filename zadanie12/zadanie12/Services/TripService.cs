using Microsoft.EntityFrameworkCore;
using zadanie12.DTOs;
using zadanie12.Models;

namespace zadanie12.Services;

public class TripService : ITripService
{
    private readonly AppDbContext _context;

    public TripService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TripDTO>> GetTripsAsync(int page = 1, int pageSize = 10)
    {
        return await _context.Trips
            .Include(t => t.CountryTrips)
            .ThenInclude(ct => ct.Country)
            .Include(t => t.ClientTrips)
            .ThenInclude(ct => ct.Client)
            .OrderByDescending(t => t.DateFrom)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new TripDTO
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.CountryTrips
                    .Select(ct => new CountryDTO { Name = ct.Country.Name })
                    .ToList(),
                Clients = t.ClientTrips
                    .Select(ct => new ClientDTO 
                    { 
                        FirstName = ct.Client.FirstName,
                        LastName = ct.Client.LastName 
                    })
                    .ToList()
            })
            .ToListAsync();
    }

    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null) return false;
        if (client.ClientTrips.Any()) return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignClientToTripAsync(int idTrip, AssignClientRequest request)
    {
        // 1. Sprawdź czy wycieczka istnieje i czy jest w przyszłości
        var trip = await _context.Trips.FindAsync(idTrip);
        if (trip == null || trip.DateFrom < DateTime.Now) return false;

        // 2. Sprawdź czy klient o podanym PESEL już istnieje
        var existingClient = await _context.Clients
            .FirstOrDefaultAsync(c => c.Pesel == request.Pesel);

        if (existingClient != null)
        {
            // 3. Sprawdź czy klient jest już zapisany na tę wycieczkę
            var existingAssignment = await _context.ClientTrips
                .FirstOrDefaultAsync(ct => ct.IdClient == existingClient.IdClient && ct.IdTrip == idTrip);

            if (existingAssignment != null) return false;
        }

        // 4. Utwórz nowego klienta jeśli nie istnieje
        var client = existingClient ?? new Client
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Telephone = request.Telephone,
            Pesel = request.Pesel,
            IdClient = 1
        };

        if (existingClient == null)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        // 5. Przypisz klienta do wycieczki
        var clientTrip = new ClientTrip
        {
            IdClient = client.IdClient,
            IdTrip = idTrip,
            RegisteredAt = DateTime.Now,
            PaymentDate = request.PaymentDate
        };

        _context.ClientTrips.Add(clientTrip);
        await _context.SaveChangesAsync();

        return true;
    }
}