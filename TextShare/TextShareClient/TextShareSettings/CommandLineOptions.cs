using CommandLine;

namespace TextShareClient.TextShareSettings;

/// <summary>
/// Base command line options for the Text Share client.
/// All other command line options should inherit from this.
/// </summary>
internal abstract class CommandLineOptions
{
    /// <summary>
    /// Copy the text entry to the clipboard. Overloads settings if set.
    /// </summary>
    [Option('c', "copy", Required = false, HelpText = "Copy the text entry to the clipboard. Overloads settings if set.")]
    public bool Copy { get; set; } = false;
}

/// <summary>
/// Opens the main menu.
/// </summary>
[Verb("menu", HelpText = "Select action from menu.")]
internal class MenuCommandLineOptions : CommandLineOptions
{

}

/// <summary>
/// Peek verb options.
/// Performs a peek action for the given id.
/// </summary>
[Verb("peek", HelpText = "Get text contents stored to id. Keeps the text on the server.")]
internal class PeekCommandLineOptions : CommandLineOptions
{
    /// <summary>
    /// Id to peek from
    /// </summary>
    [Value(0, MetaName = "id", Required = true, HelpText = "The id of the text entry to peek.")]
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Pop verb options.
/// Perfo
/// </summary>
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