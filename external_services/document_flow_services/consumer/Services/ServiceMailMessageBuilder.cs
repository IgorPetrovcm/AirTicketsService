namespace Consumer.Services;

using Consumer.Core.Model;
using Consumer.Core.Application;
using Microsoft.Extensions.Options;
using System.Net.Mime;
using Microsoft.AspNetCore.Identity;
using System.Net.Mail;

public class ServiceMailMessageBuilder : MailMessageBuilder
{
    private Stream stream;

    private ContentType type;

    private string name;

    private MailAddress from;

    private MailAddress to;

    public ServiceMailMessageBuilder(IOptions<MailMessageSettings> messageSettings) : base(messageSettings.Value)
    {
    }

    public override MailMessage Build()
    {
        MailMessage message = new MailMessage(to, from) {
            Subject = _messageSettings.Subject,
            Body = _messageSettings.Body,
        };

        Attachment attachment = new Attachment(stream, type);
        attachment.ContentDisposition.FileName = this.name;

        message.Attachments.Add(attachment);

        return message;
    }


    public void DetermineAddresses(string from, string to)
    {
        this.from = new MailAddress(from);
        this.to = new MailAddress(to);
    }

    public void Attach(Stream stream, ContentType type, string name)
    {
        this.stream = stream;
        this.type = type;
        this.name = name;
    }
}