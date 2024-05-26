using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Service.Interfaces;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeatController : ControllerBase
    {
        private readonly IFlightService flightService; 
        public SeatController(IFlightService service) 
        {
            this.flightService = service;
        }
        [HttpGet("/available/{flightId}")]
        public async Task<IActionResult> Get(int flightId) 
        {
            return Ok(await flightService.GetAvailableSeatsAsync(flightId));
        }
    }
}