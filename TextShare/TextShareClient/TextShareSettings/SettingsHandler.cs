using System.Text.Json;
using TextShareClient.TextShareSettings;

namespace TextShareClient.TextSareSettings;

internal static class SettingsHandler
{
    private const string SettingsFileName = "settings.json";

    /// <summary>
    /// Settings for this session.
    /// </summary>
    public static Settings Settings { get; private set; } = new Settings();

    /// <summary>
    /// Initialize the settings. If the settings file does not exist, it will be created.
    /// </summary>
    /// <param name="options">Command line options to influence settings</param>
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
            CopyValuesToClipboard = Settings.CopyValuesToClipboard | options.Copy,
            QuickAccessId = options.QuickAccessId ?? Settings.QuickAccessId,
            BaseAddress = options.BaseAddress ?? Settings.BaseAddress
        };
    }
}
