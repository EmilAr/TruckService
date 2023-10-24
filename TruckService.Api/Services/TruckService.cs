using TruckService.Api.Infrastructire.Filters;
using TruckService.Api.Model;
using TruckService.Api.Model.Enums;
using TruckService.Api.Model.Exceptions;
using TruckService.Api.Model.Interfaces;

namespace TruckService.Api.Services
{
    public class TruckService : ITruckService
    {
        private static readonly Dictionary<TruckStatus, List<TruckStatus>> ValidStatusTransitions = new()
        {
            { TruckStatus.OutOfService, new List<TruckStatus> { TruckStatus.Loading, TruckStatus.ToJob, TruckStatus.AtJob, TruckStatus.Returning } },
            { TruckStatus.Loading, new List<TruckStatus> { TruckStatus.ToJob } },
            { TruckStatus.ToJob, new List<TruckStatus> { TruckStatus.AtJob } },
            { TruckStatus.AtJob, new List<TruckStatus> { TruckStatus.Returning } },
            { TruckStatus.Returning, new List<TruckStatus> { TruckStatus.Loading } }
        };

        private readonly ITruckRepository _repository;

        public TruckService(ITruckRepository repository)
        {
            _repository = repository;
        }

        public async Task<Truck> CreateTruckAsynck(Truck truck)
        {
            await _repository.SaveAsync(truck);
            return truck;
        }

        public async Task DeleteTruckAsync(string truckCode)
        {
            await _repository.DeleteAsync(truckCode);
        }

        public async Task<IList<Truck>> GetAllTrucksAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<IList<Truck>> GetFilteredTrucks(TruckFilter filter)
        {
            return await _repository.GetFilterdAsync(filter);
        }

        public async Task<Truck?> GetTruckAsync(string truckCode)
        {
            return await _repository.GetAsync(truckCode);
        }

        public async Task<Truck> UpdateTruckAsync(Truck truck)
        {
            var existingTruck = await _repository.GetAsync(truck.Code) ?? throw new NotFoundException($"Truck with code ${truck.Code} not found");
            if (ValidStatusTransitions.TryGetValue(existingTruck.TruckStatus, out var validTransitions) && validTransitions.Contains(truck.TruckStatus))
            {
                existingTruck.TruckStatus = truck.TruckStatus;
                existingTruck.Name = truck.Name;
                existingTruck.Description = truck.Description;
                await _repository.UpdateAsync(existingTruck);
            }
            else
            {
                throw new InvalidOperationException($"Invalid status transition from {existingTruck.TruckStatus} to {truck.TruckStatus}");
            }
            return existingTruck;
        }
    }
}
