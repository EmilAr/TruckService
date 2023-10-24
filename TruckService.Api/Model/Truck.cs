using TruckService.Api.Model.Enums;
using TruckService.Api.Model.Interfaces;

namespace TruckService.Api.Model
{
    public class Truck : IEntity<string>
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public TruckStatus TruckStatus { get; set; }
        public string? Description { get; set; }

        public string Id => Code;
    }
}
