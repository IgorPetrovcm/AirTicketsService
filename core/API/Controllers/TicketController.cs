using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketController : ControllerBase
    {
        private readonly IFlightService flightService; 
        public TicketController(IFlightService service) 
        {
            this.flightService = service;
        }
        [HttpGet("{flightId}")]
        public async Task<IActionResult> Get(int flightId) 
        {
            return Ok(await flightService.GetTicketsAsync(flightId));
        }
    }
}