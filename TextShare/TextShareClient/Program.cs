using TextShareClient;
using TextShareClient.TextSareSettings;
using TextShareCommons.Models;
using TextCopy;

SettingsHandler.InitSettings();

var apiHandler = new ApiHandler();
var mainMenu = new Menu("Text Share Client", new[] { "Peek", "Pop", "Push", "Quick Peek", "Quick Pop", "Quick Push", "List Ids", "List Entries", "Exit" });

var mainMenuChoice = mainMenu.DisplayMenu();

switch (mainMenuChoice)
{
    case "Peek":
        await Peek();
        break;
    case "Pop":
        await Pop();
        break;
    case "List Ids":
        await ListIds();
        break;
    case "List Entries":
        await ListEntries();
        break;
    case "Push":
        await Push();
        break;
    case "Quick Peek":
        await QuickPeek();
        break;
    case "Quick Pop":
        await QuickPop();
        break;
    case "Exit":
        Console.WriteLine("Exit");
        return;
}

async Task Peek()
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

    Console.WriteLine($"Peeked text for {peekMenuChoice}:");
    var text = await apiHandler.Peek(peekMenuChoice);
    Console.WriteLine(text);

    if (SettingsHandler.Settings.CopyValuesToClipboard)
        await ClipboardService.SetTextAsync(text);
}

async Task Pop()
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

    Console.WriteLine($"Popped text for {popMenuChoice}:");
    var text = await apiHandler.Pop(popMenuChoice);
    Console.WriteLine(text);

    if (SettingsHandler.Settings.CopyValuesToClipboard)
        await ClipboardService.SetTextAsync(text);
}

async Task Push()
{
    Console.Write("Enter Id to push to: ");
    var id = Console.ReadLine();
    Console.Write("Enter text to push: ");
    var text = Console.ReadLine();

    Console.Clear();
    Console.WriteLine($"Pushing '{text}' to '{id}...'");
    var response = await apiHandler.Push(new TextEntry { Id = id!, Text = text! });
    Console.Clear();

    Console.WriteLine(response);
}

async Task QuickPeek()
{
    if(string.IsNullOrEmpty(SettingsHandler.Settings.QuickAccessId))
    {
        Console.WriteLine("No Quick Access Id set");
        return;
    }

    Console.WriteLine($"Fetching text for {SettingsHandler.Settings.QuickAccessId}...");
    var response = await apiHandler.Peek(SettingsHandler.Settings.QuickAccessId);
    Console.Clear();

    if(string.IsNullOrEmpty(response))
    {
        Console.WriteLine($"No text stored for Quick Access Id '{SettingsHandler.Settings.QuickAccessId}'");
        return;
    }

    Console.WriteLine($"Peeked text for {SettingsHandler.Settings.QuickAccessId}:");
    Console.WriteLine(response);
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
}

async Task ListIds()
{
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
        foreach(var s in texts)
            Console.WriteLine($"\t{entry.Text}");
    }
}