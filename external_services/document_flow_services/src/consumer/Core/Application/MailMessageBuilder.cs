namespace Consumer.Core.Application;

using Consumer.Core.Model;
using System.Net.Mail;


public abstract class MailMessageBuilder
{
    protected readonly MailMessageSettings _messageSettings;

    public MailMessageBuilder(MailMessageSettings messageSettings)
    {
        _messageSettings = messageSettings;
    }

    public virtual MailMessage Build()
    {
        return new MailMessage(){ Subject = _messageSettings.Subject, Body = _messageSettings.Body };
    }
}