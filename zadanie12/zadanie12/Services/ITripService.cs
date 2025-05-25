using zadanie12.DTOs;

namespace zadanie12.Services;

public interface ITripService
{
    Task<IEnumerable<TripDTO>> GetTripsAsync(int page, int pageSize);
    Task<bool> DeleteClientAsync(int idClient);
    Task<bool> AssignClientToTripAsync(int idTrip, AssignClientRequest request);
}