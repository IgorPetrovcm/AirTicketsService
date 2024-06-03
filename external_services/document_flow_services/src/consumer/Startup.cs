using System.Text;
using Microsoft.Extensions.FileProviders;

namespace Consumer;

public class Startup 
{
    public static DirectoryInfo GetMailConfiguration()
    {
        string mailConfigurationPath =
                Environment.OSVersion.Platform == PlatformID.Unix || Environment.OSVersion.Platform == PlatformID.Other
                ? Environment.GetEnvironmentVariable("HOME")
                : Environment.GetEnvironmentVariable("%HOMEDRIVE%%HOMEPATH%");


        return new DirectoryInfo(mailConfigurationPath + "/.airtickets_mailconf/");
    }
}