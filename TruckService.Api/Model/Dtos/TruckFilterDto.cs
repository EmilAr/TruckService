using TruckService.Api.Model.Enums;

namespace TruckService.Api.Model.Dtos
{
    public class TruckFilterDto
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public TruckStatus? TruckStatus { get; set; }
        public string? Description { get; set; }
        public IList<OrderFilterDto>? OrderFilterDtos { get; set; }
    }

    public class OrderFilterDto
    {
        public required OrderProperty OrderBy { get; set; }
        public required bool Ascending { get; set; } = true;
    }
}
