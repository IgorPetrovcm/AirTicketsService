using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly IFlightService flightService; 
        public BookingController(IFlightService service) 
        {
            this.flightService = service;
        }
        [HttpPost]
        public async Task<IActionResult> Create() 
        {
            return Ok(await flightService.CreateBooking());
        }
    }
}