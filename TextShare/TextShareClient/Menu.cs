namespace TextShareClient;

/// <summary>
/// Represents a menu with custom choices. Provides method to present menu and get user choice.
/// </summary>
internal class Menu
{
    private List<string> _options = new List<string>();
    private string _greeting = string.Empty;

    /// <summary>
    /// Creates a new instance of the Menu class.
    /// </summary>
    /// <param name="greeting">Text to display before the menu.</param>
    /// <param name="options">Collection of menu items to choose from.</param>
    public Menu(string greeting, IEnumerable<string> options)
    {
        _options.AddRange(options);
        _greeting = greeting;
    }

    /// <summary>
    /// Displays a menu to the user and returns the selected option.
    /// </summary>
    /// <param name="clearScreenPreMenu">Clear the screen before the menu. Default: true</param>
    /// <param name="clearScreenPostMenu">Claear the screen after the menu. Default: true</param>
    /// <returns>String with the text of the choosen option</returns>
    /// <exception cref="InvalidOperationException"></exception>
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
