using TextShareClient;
using TextShareClient.TextSareSettings;
using TextShareCommons.Models;
using TextCopy;
using CommandLine;
using TextShareClient.TextShareSettings;

var parsedOptions = Parser.Default.ParseArguments<MenuCommandLineOptions, PeekCommandLineOptions,
                                                PopCommandLineOptions, PushCommandLineOptions,
                                                ListIdsCommandLineOptions, ListEntriesCommandLineOptions>(args).Value;

if (parsedOptions is null)
    return;

SettingsHandler.InitSettings((CommandLineOptions)parsedOptions);
var apiHandler = new ApiHandler();


switch (parsedOptions)
{
    case MenuCommandLineOptions:
        await MainMenu();
        break;
    case PeekCommandLineOptions options:
        await Peek(options.Id);
        break;
    case PopCommandLineOptions options:
        await Pop(options.Id);
        break;
    case PushCommandLineOptions options:
        await Push((new TextEntry { Id = options.Id, Text = options.Text }));
        break;
    case ListIdsCommandLineOptions:
        await ListIds();
        break;
    case ListEntriesCommandLineOptions:
        await ListEntries();
        break;
}

async Task MainMenu()
{
    var mainMenu = new Menu("Text Share Client", new[] { "Peek", "Pop", "Push",
                                                        "Quick Peek", "Quick Pop", "Quick Push",
                                                            "List Ids", "List Entries",
                                                            "Show Settings", "Exit" });
    var mainMenuChoice = mainMenu.DisplayMenu();

    switch (mainMenuChoice)
    {
        case "Peek":
            await PeekMenu();
            break;
        case "Pop":
            await PopMenu();
            break;
        case "List Ids":
            await ListIds();
            break;
        case "List Entries":
            await ListEntries();
            break;
        case "Push":
            await PushMenu();
            break;
        case "Quick Peek":
            await QuickPeek();
            break;
        case "Quick Pop":
            await QuickPop();
            break;
        case "Quick Push":
            await QuickPush();
            break;
        case "Show Settings":
            ShowSettings();
            break;
        case "Exit":
            Console.WriteLine("Exit");
            return;
    }
}

async Task PeekMenu()
{
    Console.WriteLine("Fetching Ids...");
    var ids = await apiHandler.ListIds();
    Console.Clear();

    if (ids.Length == 0)
    {
        Console.WriteLine("No Ids to peek");
        return;
    }

    var peekMenu = new Menu("Peek what Id?", ids);
    var peekMenuChoice = peekMenu.DisplayMenu();

    await Peek(peekMenuChoice);
}

async Task Peek(string id)
{
    Console.WriteLine($"Fetching text for '{id}'");
    var text = await apiHandler.Peek(id);
    Console.Clear();

    if (string.IsNullOrEmpty(text))
    {
        Console.WriteLine($"No text stored for Id '{id}'");
        return;
    }

    Console.WriteLine($"Peeked text for {id}:");
    Console.WriteLine(text);

    if (SettingsHandler.Settings.CopyValuesToClipboard)
        await ClipboardService.SetTextAsync(text);
}

async Task PopMenu()
{
    Console.WriteLine("Fetching Ids...");
    var ids = await apiHandler.ListIds();
    Console.Clear();

    if (ids.Length == 0)
    {
        Console.WriteLine("No Ids to pop");
        return;
    }

    var popMenu = new Menu("Pop what Id?", ids);
    var popMenuChoice = popMenu.DisplayMenu();

    await Pop(popMenuChoice);
}

async Task Pop(string id)
{
    Console.WriteLine($"Popping text for {id}");
    var text = await apiHandler.Pop(id);
    Console.Clear();

    if (string.IsNullOrEmpty(text))
    {
        Console.WriteLine($"No text stored for Quick Access Id '{SettingsHandler.Settings.QuickAccessId}'");
        return;
    }

    Console.WriteLine($"Popped text for {id}:");
    Console.WriteLine(text);

    if (SettingsHandler.Settings.CopyValuesToClipboard)
        await ClipboardService.SetTextAsync(text);
}

async Task PushMenu()
{
    Console.Write("Enter Id to push to: ");
    var id = Console.ReadLine();
    Console.Write("Enter text to push: ");
    var text = Console.ReadLine();

    await Push(new TextEntry { Id = id!, Text = text! });
}

async Task Push(TextEntry textEntry)
{
    Console.Clear();
    Console.WriteLine($"Pushing '{textEntry.Text}' to '{textEntry.Id}'...");
    var response = await apiHandler.Push(textEntry);
    Console.Clear();

    Console.WriteLine(response);
}

async Task QuickPeek()
{
    if (string.IsNullOrEmpty(SettingsHandler.Settings.QuickAccessId))
    {
        Console.WriteLine("No Quick Access Id set");
        return;
    }

    Console.WriteLine($"Fetching text for {SettingsHandler.Settings.QuickAccessId}...");
    var response = await apiHandler.Peek(SettingsHandler.Settings.QuickAccessId);
    Console.Clear();

    if (string.IsNullOrEmpty(response))
    {
        Console.WriteLine($"No text stored for Quick Access Id '{SettingsHandler.Settings.QuickAccessId}'");
        return;
    }

    Console.WriteLine($"Peeked text for {SettingsHandler.Settings.QuickAccessId}:");
    Console.WriteLine(response);

    if (SettingsHandler.Settings.CopyValuesToClipboard)
        await ClipboardService.SetTextAsync(response);
}

async Task QuickPop()
{
    if (string.IsNullOrEmpty(SettingsHandler.Settings.QuickAccessId))
    {
        Console.WriteLine("No Quick Access Id set");
        return;
    }

    Console.WriteLine($"Fetching text for {SettingsHandler.Settings.QuickAccessId}...");
    var response = await apiHandler.Pop(SettingsHandler.Settings.QuickAccessId);
    Console.Clear();

    if (string.IsNullOrEmpty(response))
    {
        Console.WriteLine($"No text stored for Quick Access Id '{SettingsHandler.Settings.QuickAccessId}'");
        return;
    }

    Console.WriteLine($"Popped text for {SettingsHandler.Settings.QuickAccessId}:");
    Console.WriteLine(response);

    if (SettingsHandler.Settings.CopyValuesToClipboard)
        await ClipboardService.SetTextAsync(response);
}

async Task QuickPush()
{
    if (string.IsNullOrEmpty(SettingsHandler.Settings.QuickAccessId))
    {
        Console.WriteLine("No Quick Access Id set");
        return;
    }

    Console.Write($"Enter text to push to {SettingsHandler.Settings.QuickAccessId}: ");
    var text = Console.ReadLine();

    Console.Clear();
    Console.WriteLine($"Pushing '{text}' to '{SettingsHandler.Settings.QuickAccessId}'...");
    var response = await apiHandler.Push(new TextEntry { Id = SettingsHandler.Settings.QuickAccessId, Text = text! });
    Console.Clear();

    Console.WriteLine(response);
}

async Task ListIds()
{
    Console.WriteLine("Fetching ids...");
    var ids = await apiHandler.ListIds();
    Console.Clear();

    if (ids.Length == 0)
    {
        Console.WriteLine("No Ids stored");
        return;
    }

    Console.WriteLine("List Ids:");
    foreach (var id in ids)
    {
        Console.WriteLine(id);
    }
}

async Task ListEntries()
{
    Console.WriteLine("Fetching entries...");
    var entries = await apiHandler.ListEntries();
    Console.Clear();

    if (entries.Length == 0)
    {
        Console.WriteLine("No Entries stored");
        return;
    }
    Console.WriteLine("List Entries:");
    foreach (var entry in entries)
    {
        Console.WriteLine($"{entry.Id}:");
        var texts = entry.Text.Replace("\r", "").Split("\n");
        foreach (var s in texts)
            Console.WriteLine($"\t{entry.Text}");
    }
}

void ShowSettings()
{
    Console.WriteLine($"Base Adress: '{SettingsHandler.Settings.BaseAddress}'");
    Console.WriteLine($"Quick Access Id: '{SettingsHandler.Settings.QuickAccessId}'");
    Console.WriteLine($"Copy Values to Clipboard: '{SettingsHandler.Settings.CopyValuesToClipboard}'");
}