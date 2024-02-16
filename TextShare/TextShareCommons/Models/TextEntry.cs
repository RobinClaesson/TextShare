namespace TextShareCommons.Models;

/// <summary>
/// Reporesents a text entry on the server with an id and text.
/// </summary>
public record TextEntry
{
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
