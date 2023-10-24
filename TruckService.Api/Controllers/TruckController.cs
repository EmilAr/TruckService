using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TruckService.Api.Infrastructire.Filters;
using TruckService.Api.Model;
using TruckService.Api.Model.Dtos;
using TruckService.Api.Model.Exceptions;
using TruckService.Api.Model.Interfaces;

namespace TruckService.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TruckController : ControllerBase
    {
        private readonly ITruckService _truckService;
        private readonly IMapper _mapper;

        public TruckController(ITruckService truckService, IMapper mapper)
        {
            _truckService = truckService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var truck = await _truckService.GetTruckAsync(id);
            return truck is null ? NotFound() : Ok(_mapper.Map<TruckDto>(truck));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trucks = await _truckService.GetAllTrucksAsync();
            return Ok(_mapper.Map<IList<TruckDto>>(trucks));
        }

        [HttpPost]
        public async Task<IActionResult> Create(TruckDto truckDto)
        {
            var truck = await _truckService.CreateTruckAsynck(_mapper.Map<Truck>(truckDto));
            return CreatedAtAction(nameof(Get), new { id = truck.Code }, _mapper.Map<TruckDto>(truck));
        }

        [HttpPut]
        public async Task<IActionResult> Update(TruckDto truckDto)
        {
            try
            {
                await _truckService.UpdateTruckAsync(_mapper.Map<Truck>(truckDto));
                return NoContent();
            }
            catch (NotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _truckService.DeleteTruckAsync(id);
            return Ok();
        }

        [HttpPost("search")]
        public async Task<IActionResult> Search(TruckFilterDto truckFilterDto)
        {
            var trucks = await _truckService.GetFilteredTrucks(new TruckFilter(truckFilterDto));
            return Ok(_mapper.Map<IList<TruckDto>>(trucks));
        }
    }
}
