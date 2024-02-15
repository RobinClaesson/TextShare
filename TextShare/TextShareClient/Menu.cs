namespace TextShareClient;

internal class Menu
{
    private List<string> _options = new List<string>();
    private string _greeting = string.Empty;

    public Menu(string greeting, IEnumerable<string> options)
    {
        _options.AddRange(options);
        _greeting = greeting;
    }

    public string DisplayMenu(bool clearScreenPreMenu = true, bool clearScreenPostMenu = true)
    {
        if (_options.Count == 0)
        {
            throw new InvalidOperationException("No options available to display");
        }

        if (clearScreenPreMenu)
        {
            Console.Clear();
        }

        Console.WriteLine(_greeting);
        
        for (int i = 0; i < _options.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_options[i]}");
        }

        Console.WriteLine();
        var coice = 0;
        do
        {
            Console.Write($"Please choose an option (1-{_options.Count}): ");
            coice = int.Parse(Console.ReadLine()!);
        } while (coice < 1 || coice > _options.Count);

        if(clearScreenPostMenu)
        {
            Console.Clear();
        }

        return _options[coice - 1];
    }

}
