using Microsoft.AspNetCore.Authentication.OAuth.Claims;
using Microsoft.AspNetCore.Mvc.Formatters;

namespace TextShareServer.Services;

public class FileService
{
    public const string TextStorageFolder = "StoredTexts";

    public FileService()
    {
        if (!Directory.Exists(TextStorageFolder))
        {
            Directory.CreateDirectory(TextStorageFolder);
        }
    }

    public string GetTextFilePath(string id)
        => Path.Combine(TextStorageFolder, id + ".txt");


    public bool FileExists(string id)
        => File.Exists(GetTextFilePath(id));

    public string GetText(string id)
    {
        var filePath = GetTextFilePath(id);
        if (!File.Exists(filePath))
        {
            return string.Empty;
        }
        return File.ReadAllText(filePath);
    }

    public void StoreText(string id, string text)
    {
        var filePath = GetTextFilePath(id);
        using (var writer = new StreamWriter(filePath, true))
        {
            writer.WriteLine(text);
        }
    }

    public void DeleteText(string id)
    {
        var filePath = GetTextFilePath(id);
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }
    }
}
