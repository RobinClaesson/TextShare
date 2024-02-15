using System.Text.Json;
using TextShareClient.TextShareSettings;

namespace TextShareClient.TextSareSettings;

internal static class SettingsHandler
{
    private const string SettingsFileName = "settings.json";

    public static Settings Settings { get; private set; } = new Settings();

    public static void InitSettings(CommandLineOptions options)
    {
        if (File.Exists(SettingsFileName))
        {
            Settings = JsonSerializer.Deserialize<Settings>(File.ReadAllText(SettingsFileName)) is Settings s ? s : new();
        }
        else
        {
            File.WriteAllText(SettingsFileName, JsonSerializer.Serialize(Settings));
        }

        Settings = Settings with
        {
            CopyValuesToClipboard = Settings.CopyValuesToClipboard | options.Copy
        };
    }
}
