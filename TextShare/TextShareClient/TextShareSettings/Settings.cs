using TextShareCommons;

namespace TextShareClient.TextSareSettings;

internal class Settings
{
    public string BaseAddress { get; init; } = $"http://localhost:{Globals.DefaultHttpPort}";
    public string QuickAccessId { get; init; } = string.Empty;
    public bool CopyValuesToClipboard { get; init; } = false;
}
