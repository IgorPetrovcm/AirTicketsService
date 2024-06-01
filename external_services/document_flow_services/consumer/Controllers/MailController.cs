namespace Consumer.Controllers;

using Microsoft.AspNetCore.Mvc;
using Consumer.Core.Application;

[ApiController]
[Route("api/[controller]")]
public class MailController : ControllerBase
{
    private readonly IMailHost _mailHost;

    public MailController( IMailHost mailHost)
    {
        _mailHost = mailHost;
    }

    [HttpGet]
    [Route("[action]")]
    public IActionResult SendMessage([FromQuery]string message, [FromQuery]string destination)
    {
        Task.Run( () => _mailHost.SendMessage(message, destination));

        return NoContent();
    }
}