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
/// Command line options to show main menu.
/// </summary>
[Verb("menu", HelpText = "Select action from menu. Usage: TextShareClient menu")]
internal class MenuCommandLineOptions : CommandLineOptions
{

}

/// <summary>
/// Command line options to perform a peek.
/// </summary>
[Verb("peek", HelpText = "Get text contents stored to id. Keeps the text on the server. Usage: TextShareClient peek <id>")]
internal class PeekCommandLineOptions : CommandLineOptions
{
    /// <summary>
    /// Id to peek text from.
    /// </summary>
    [Value(0, MetaName = "id", Required = true, HelpText = "The id of the text entry to peek.")]
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Command line option to perform a pop.
/// </summary>
[Verb("pop", HelpText = "Get text contents stored to id. Deletes the text from the server. Usage: TextShareClient pop <id>")]
internal class PopCommandLineOptions : CommandLineOptions
{
    /// <summary>
    /// Id to pop text from.
    /// </summary>
    [Value(0, MetaName = "id", Required = true, HelpText = "The id of the text entry to pop.")]
    public string Id { get; set; } = string.Empty;
}

/// <summary>
/// Command line option to perform a push. 
/// </summary>
[Verb("push", HelpText = "Stores text to id. Usage: TextShareClient push <id> <text>")]
internal class PushCommandLineOptions : CommandLineOptions
{
    /// <summary>
    /// Id to push text to.
    /// </summary>
    [Value(0, MetaName = "id", Required = true, HelpText = "The id of the text entry to push.")]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Text to push to the id. 
    /// </summary>
    [Value(1, MetaName = "text", Required = true, HelpText = "The text to store.")]
    public string Text { get; set; } = string.Empty;
}

/// <summary>
/// Command line option to list all ids.
/// </summary>
[Verb("list-id", HelpText = "List all ids. Usage: TextShareClient list-id")]
internal class ListIdsCommandLineOptions : CommandLineOptions { }

/// <summary>
/// Command line option to list all entries.
/// </summary>
[Verb("list-entries", HelpText = "List all entries. Usage: TextShareClient list-entries")]
internal class ListEntriesCommandLineOptions : CommandLineOptions { }