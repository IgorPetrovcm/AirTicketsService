using Microsoft.AspNetCore.Mvc;

public class HomeController : ControllerBase
{
    private readonly AppDbContext dbContext;
    public HomeController(AppDbContext dbContext) {
        this.dbContext = dbContext;
    }

    [HttpGet("/flight")]
    public ActionResult<List<Flight>> GetFlight() {
        return dbContext.Flights.ToList();
    }
}