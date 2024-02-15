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
        Console.WriteLine("List Entries");
        break;
    case "Push":
        await Push();
        break;
    case "Exit":
        Console.WriteLine("Exit");
        return;
}

async Task Peek()
{
    Console.WriteLine("Fetching Ids...");
    var ids = await apiHandler.ListIds();

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

    var response = await apiHandler.Push(new TextEntry { Id = id!, Text = text! });
    Console.WriteLine(response);
}

async Task ListIds()
{
    var ids = await apiHandler.ListIds();

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