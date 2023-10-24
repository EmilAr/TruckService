using AutoMapper;
using TruckService.Api.Infrastructire.Filters;
using TruckService.Api.Model.Dtos;

namespace TruckService.Api.Model
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Truck, TruckDto>().ReverseMap();
        }
    }
}
