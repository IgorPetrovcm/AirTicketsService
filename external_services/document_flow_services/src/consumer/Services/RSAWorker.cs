namespace Consumer.Services;

using Consumer.Core.Application;
using System.Security.Cryptography;
using System.Text;

public class RSAWorker : IRSAWorker
{
    // private readonly string _privateKey;

    private readonly RSACryptoServiceProvider _rsaProvider;

    public RSAWorker() {}

    public RSAWorker(int keySize, string privateKey)
    {
        _rsaProvider = new RSACryptoServiceProvider( keySize);

        _rsaProvider.ImportFromPem( privateKey );
    }

    public string Decrypt( string value )
    {
        byte[] valueBytes = Convert.FromBase64String(value);

        return Encoding.UTF8.GetString(
                _rsaProvider.Decrypt( valueBytes, true )
            );
    }
}