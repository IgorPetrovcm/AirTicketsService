namespace Producer.Controllers;

using Producer.Core.Application;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class MessageSendingController : ControllerBase
{
    private readonly IMessageSendingWorker _messageWorker;

    public MessageSendingController( IMessageSendingWorker messageWorker)
    {
        _messageWorker = messageWorker;
    }

    [HttpPost]
    [Route("[action]")]
    public IActionResult Send([FromBody] TicketRequestDTO requestMessage)
    {
        _messageWorker.SendMessageAsync( requestMessage );

        return NoContent();
    }
}