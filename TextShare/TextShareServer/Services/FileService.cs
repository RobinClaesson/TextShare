using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace TextShareServer.Services;

public class FileService
{
    public const string TextStorageFolder = "StoredTexts";
    public const string TextFileExtension = ".txt";

    public FileService()
    {
        if (!Directory.Exists(TextStorageFolder))
        {
            Directory.CreateDirectory(TextStorageFolder);
        }
    }

    /// <summary>
    /// Gets the file path for the given id.
    /// </summary>
    /// <param name="id">Id to get the path for.</param>
    /// <returns>Relative path to the text file for id.</returns>
    private string GetTextFilePath(string id)
        => Path.Combine(TextStorageFolder, id + TextFileExtension);

    /// <summary>
    /// Determines if the specified id contains text.
    /// </summary>
    /// <param name="id">Id to check</param>
    /// <returns>True if the given id has stored text, false otherwise.</returns>
    public bool HasText(string id)
        => File.Exists(GetTextFilePath(id));

    /// <summary>
    /// Get all id's with stored text.
    /// </summary>
    /// <returns>Collection of all id with stored text.</returns>
    public IEnumerable<string> GetAllId()
        => Directory.GetFiles(TextStorageFolder, $"*{TextFileExtension}")
            .Select(Path.GetFileName)
            .Select(x => x!.Replace(TextFileExtension, string.Empty));

    /// <summary>
    /// Gets the text stored with an id.
    /// </summary>
    /// <param name="id">Id to retrieve text from.</param>
    /// <returns>Text stored with the id if it exists, empty string otherwise.</returns>
    public string GetText(string id)
    {
        var filePath = GetTextFilePath(id);
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }
        return File.ReadAllText(filePath);
    }

    /// <summary>
    /// Store text with for an id. Appends if text already existed. 
    /// </summary>
    /// <param name="id">Id to store text for.</param>
    /// <param name="text">Text to store.</param>
    public void StoreText(string id, string text)
    {
        var filePath = GetTextFilePath(id);
        using (var writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(text);
        }
    }

    /// <summary>
    /// Deletes text stored with an id.
    /// </summary>
    /// <param name="id">Id of the text to delete.</param>
    public void DeleteText(string id)
    {
        var filePath = GetTextFilePath(id);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
