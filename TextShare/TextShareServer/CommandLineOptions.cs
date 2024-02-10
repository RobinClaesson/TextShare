using CommandLine;

namespace TextShareServer;

public class CommandLineOptions
{
    [Option("swagger", Required = false, HelpText = "Allows direct API access with Swagger")]
    public bool UseSwagger { get; set; }

    [Option('p', "http-port", Required = false, HelpText = "The port to listen for HTTP requests. Default value: '5000'")]
    public int HttpPort { get; set; } = 5000;

    [Option('s', "https-port", Required = false, HelpText = "The port to listen for HTTPS requests. HTTPS turned of if not set")]
    public int HttpsPort { get; set; } = -1;
    public bool UseHttps => HttpsPort != -1;

    [Option('l', "localhost", Required = false, HelpText = "Allow connection only from localhost")]
    public bool LocalHost { get; set; } = false;

    public string[] GetHostUrls()
    {
        var host = LocalHost ? "localhost" : "[::]";
        return UseHttps ? new string[] { $"http://{host}:{HttpPort}", $"https://{host}:{HttpsPort}" } : new string[] { $"http://{host}:{HttpPort}" };
    }
}
