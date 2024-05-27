using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService flightService; 
        public FlightController(IFlightService service) 
        {
            this.flightService = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            return Ok(await flightService.GetAllAsync());
        }

        [HttpGet("{departureDate}")]
        public async Task<IActionResult> GetByDepartureDate(DateOnly departureDate)
        {
            return Ok(await flightService.GetByDepartureDateAsync(departureDate));
        }
    }
}