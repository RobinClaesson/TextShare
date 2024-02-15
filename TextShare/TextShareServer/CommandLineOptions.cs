using CommandLine;
using TextShareCommons;

namespace TextShareServer;

public class CommandLineOptions
{
    [Option('p', "http-port", Required = false, HelpText = $"The port to listen for HTTP requests. Default value: '5000'.")]
    public int HttpPort { get; set; } = Globals.DefaultHttpPort;

    [Option('s', "https-port", Required = false, HelpText = "The port to listen for HTTPS requests. HTTPS turned of if not set.")]
    public int? HttpsPort { get; set; } = null;
    public bool UseHttps => HttpsPort != null;

    [Option("no-http", Required = false, HelpText = "Turn off HTTP. Requires HTTPS port to be set.")]
    public bool NoHttp { get; set; } = false;

    [Option('l', "localhost", Required = false, HelpText = "Allow connection only from localhost.")]
    public bool LocalHost { get; set; } = false;

    [Option('w', "swagger", Required = false, HelpText = "Allows direct API access with Swagger.")]
    public bool UseSwagger { get; set; }

    public string[] GetHostUrls()
    {
        var host = LocalHost ? "localhost" : "[::]";

        //Niether HTTP or HTTPS are enabled
        if(NoHttp && !UseHttps)
            throw new ArgumentException("No HTTP is enabled, but HTTPS is not. This will result in no access to the server.");

        //HTTP is enabled, but HTTPS is not
        if(!NoHttp && !UseHttps)
            return new string[] { $"http://{host}:{HttpPort}" };

        //HTTPS is enabled, but HTTP is not
        if(NoHttp && UseHttps)
            return new string[] { $"https://{host}:{HttpsPort}" };

        //Both HTTP and HTTPS are enabled
        return new string[] { $"http://{host}:{HttpPort}", $"https://{host}:{HttpsPort}" };
    }

}
