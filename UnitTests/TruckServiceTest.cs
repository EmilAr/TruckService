using NSubstitute;
using TruckService.Api.Model;
using TruckService.Api.Model.Enums;
using TruckService.Api.Model.Interfaces;

namespace UnitTests
{
    public class TruckServiceTest
    {
        [Fact]
        public async Task UpdateTruckTest_FromOutOfServiceToAtJob()
        {
            var repository = PrepareMockedRepositoryForUpdate(TruckStatus.OutOfService);
            var truckService = new TruckService.Api.Services.TruckService(repository);

            var newTruck = new Truck
            {
                Code = "123",
                Description = "NewDescription",
                Name = "NewName",
                TruckStatus = TruckStatus.AtJob
            };

            var updatedValue = await truckService.UpdateTruckAsync(newTruck);
            Assert.NotNull(updatedValue);
            Assert.Equal(newTruck.Name, updatedValue.Name);
            Assert.Equal(newTruck.Description, updatedValue.Description);
            Assert.Equal(newTruck.TruckStatus, updatedValue.TruckStatus);
        }

        [Fact]
        public async Task UpdateTruckTest_FromAtJobToLoading()
        {
            var repository = PrepareMockedRepositoryForUpdate(TruckStatus.AtJob);
            var truckService = new TruckService.Api.Services.TruckService(repository);

            var newTruck = new Truck
            {
                Code = "123",
                Description = "NewDescription",
                Name = "NewName",
                TruckStatus = TruckStatus.Loading
            };

            await Assert.ThrowsAsync<InvalidOperationException>(() => truckService.UpdateTruckAsync(newTruck));
        }

        private static ITruckRepository PrepareMockedRepositoryForUpdate(TruckStatus startingTruckStatus)
        {
            var truck = new Truck
            {
                Code = "123",
                Description = "Description",
                Name = "Name",
                TruckStatus = startingTruckStatus
            };
            var repository = Substitute.For<ITruckRepository>();
            repository.GetAsync(Arg.Any<string>()).Returns(Task.FromResult<Truck?>(truck));
            repository.UpdateAsync(Arg.Any<Truck>()).Returns(x => Task.FromResult(x[0]));
            return repository;
        }
    }
}