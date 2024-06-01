namespace Consumer.Services;

using Consumer.Core.Application;
using Consumer.Core.Model;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Security.Cryptography;

public class MailHost : IMailHost
{
    private readonly MailConfiguration _mail;

    private readonly IRSAWorker _rsaWorker;

    private readonly ServiceMailMessageBuilder _messageBuilder;

    private readonly FileGenerator _fileGenerator;

    private SmtpClient client;

    public MailHost(
        IOptions<MailConfiguration> mailOption, 
        IRSAWorker rsaWorker, 
        ServiceMailMessageBuilder messageBuilder,
        FileGenerator fileGenerator
    )
    {
        _mail = mailOption.Value;

        _rsaWorker = rsaWorker;

        _messageBuilder = messageBuilder;

        _fileGenerator = fileGenerator;

        client = new SmtpClient("smtp.mail.ru", 587);

        // string? encryptPassword = mailOption.Value.Password;
        
        // if (encryptPassword == null || encryptPassword.Trim() == ""){
        //     throw new ArgumentNullException("Encrypted password is null");
        // }

        // byte[] encryptPasswordBytes = Convert.FromBase64String(encryptPassword);

        // using RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

        // rsa.ImportFromPem( File.ReadAllText(fileWithPrivateKey.FullName) );

        // _mail.Password = Encoding.UTF8.GetString( rsa.Decrypt( encryptPasswordBytes, true) );

        // _mail.Address = mailOption.Value.Address;
    }

    object blocking;

    public void SendMessage(string message, string destination)
    {
        client.Credentials = new NetworkCredential( _mail.Address, _rsaWorker.Decrypt( _mail.Password ) );

        client.EnableSsl = true;

        _messageBuilder.DetermineAddresses( _mail.Address, destination );

        ContentType type = new ContentType( MediaTypeNames.Application.Pdf );
        Stream stream = _fileGenerator.GeneratePdf( message );
    
        _messageBuilder.Attach( stream, type, "Ticket.pdf");
        try 
        {
            MailMessage messagem = _messageBuilder.Build();
            client.Send( messagem );
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
        stream.Close();
        stream.Dispose();

    }

    public Task SendMessageAsync(string message, string destination)
    {
        return new Task(() => SendMessage( message, destination));
    }
}