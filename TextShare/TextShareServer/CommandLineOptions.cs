using CommandLine;
using CommandLine.Text;

namespace TextShareServer;

public class CommandLineOptions
{
    [Option("swagger", Required = false, HelpText = "Allows direct API access with Swagger")]
    public bool UseSwagger { get; set; }

}
