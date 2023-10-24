using TruckService.Api.Infrastructire.Filters;

namespace TruckService.Api.Model.Interfaces
{
    public interface ITruckService
    {
        Task<Truck> CreateTruckAsynck(Truck truck);
        Task<IList<Truck>> GetAllTrucksAsync();
        Task<Truck?> GetTruckAsync(string truckCode);
        Task DeleteTruckAsync(string truckCode);
        Task<Truck> UpdateTruckAsync(Truck truck);
        Task<IList<Truck>> GetFilteredTrucks(TruckFilter filter);
    }
}
