using TextShareCommons;

namespace TextShareClient.TextSareSettings;

/// <summary>
/// Represents the settings stored on file for the Text Share client.
/// </summary>
internal record Settings
{
    /// <summary>
    /// Base adress of the Text Share server.
    /// </summary>
    public string BaseAddress { get; init; } = $"http://localhost:{Globals.DefaultHttpPort}";

    /// <summary>
    /// Id to use for qucick peek/pop/push.
    ///</summary>
    public string QuickAccessId { get; init; } = string.Empty;

    /// <summary>
    /// True if the client should copy the text to the clipboard after a peek or pop.
    /// </summary>
    public bool CopyValuesToClipboard { get; init; } = false;
}
