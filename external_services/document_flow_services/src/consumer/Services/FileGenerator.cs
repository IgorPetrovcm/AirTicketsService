using System.Text;
using Microsoft.AspNetCore.SignalR.Protocol;

namespace Consumer.Services;


public class FileGenerator
{

    public Stream GeneratePdf(string message)
    {
        FileStream stream = new FileStream("ticket.pdf", FileMode.OpenOrCreate, FileAccess.ReadWrite);

        stream.SetLength(0);

        byte[] messageBytes = Encoding.UTF8.GetBytes(message);

        stream.Write( messageBytes, 0, messageBytes.Length);

        return stream;
    }
}