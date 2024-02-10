namespace TextShareServer;

public static class ConsoleInfo
{
    public static void WriteInfo<T>(T value)
    {
        var prevColor = Console.ForegroundColor;
        var prevBgColor = Console.BackgroundColor;
        
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("info");
        Console.ForegroundColor = prevColor;
        Console.Write(": ");

        Console.WriteLine(value);
    }
}
