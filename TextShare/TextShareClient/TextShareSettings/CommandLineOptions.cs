using CommandLine;

namespace TextShareClient.TextShareSettings;

internal abstract class CommandLineOptions
{
    [Option('c', "copy", Required = false, HelpText = "Copy the text entry to the clipboard. Overloads settings if set.")]
    public bool Copy { get; set; } = false;
}

[Verb("menu", isDefault: true, HelpText = "Select action from menu.")]
internal class MenuCommandLineOptions : CommandLineOptions
{

}

[Verb("peek", HelpText = "Get text contents stored to id. Keeps the text on the server.")]
internal class PeekCommandLineOptions : CommandLineOptions
{
    [Value(0, MetaName = "id", Required = true, HelpText = "The id of the text entry to peek.")]
    public string Id { get; set; } = string.Empty;
}

[Verb("pop", HelpText = "Get text contents stored to id. Deletes the text from the server")]
internal class PopCommandLineOptions : CommandLineOptions
{
    [Value(0, MetaName = "id", Required = true, HelpText = "The id of the text entry to pop.")]
    public string Id { get; set; } = string.Empty;
}

[Verb("push", HelpText = "Stores text to id.")]
internal class PushCommandLineOptions : CommandLineOptions
{
    [Value(0, MetaName = "id", Required = true, HelpText = "The id of the text entry to push.")]
    public string Id { get; set; } = string.Empty;
    [Value(1, MetaName = "text", Required = true, HelpText = "The text to store.")]
    public string Text { get; set; } = string.Empty;
}

[Verb("list", HelpText = "List all ids.")]
internal class ListIdsCommandLineOptions : CommandLineOptions { }

[Verb("list-entries", HelpText = "List all entries.")]
internal class ListEntriesCommandLineOptions : CommandLineOptions { }