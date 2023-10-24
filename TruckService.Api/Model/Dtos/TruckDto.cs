using TruckService.Api.Model.Enums;

namespace TruckService.Api.Model.Dtos
{
    public class TruckDto
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public TruckStatus TruckStatus { get; set; }
        public string? Description { get; set; }
    }
}
