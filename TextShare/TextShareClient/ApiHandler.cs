using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TextShareClient.TextSareSettings;
using TextShareCommons.Models;

namespace TextShareClient;

/// <summary>
/// A handler for the Text Share API. Provides methods to interact with the API.
/// </summary>
internal class ApiHandler
{
    private HttpClient _httpClient;
    
    public ApiHandler()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(SettingsHandler.Settings.BaseAddress) };
    }

    /// <summary>
    /// Gets a list of all available ids on the server.
    /// </summary>
    /// <returns>Array of all ids stored on the server. </returns>
    public async Task<string[]> ListIds()
    {
        var response = await _httpClient.GetFromJsonAsync<string[]>("/Text/ListIds");
        return response ?? new string[0];
    }

    /// <summary>
    /// Gets a list of all available entries with id and stored text on the server.
    /// </summary>
    /// <returns>Array of all entries stored on the server. </returns>
    public async Task<TextEntry[]> ListEntries()
    {
        var response = await _httpClient.GetFromJsonAsync<TextEntry[]>("/Text/ListEntries");
        return response ?? new TextEntry[0];
    }

    /// <summary>
    /// Gets the text stored with an id without removing it from the server.
    /// </summary>
    /// <param name="id">Id of the text to peek.</param>
    /// <returns>Text stored with the id on the server. </returns>
    public async Task<string> Peek(string id)
    {
        var response = await _httpClient.GetAsync($"/Text/Peek/{id}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync() ?? string.Empty;

        return string.Empty;
    }

    /// <summary>
    /// Gets the text stored with an id and removes it from the server.
    /// </summary>
    /// <param name="id">Id of the text to pop</param>
    /// <returns>Text stored with the id on the server</returns>
    public async Task<string> Pop(string id)
    {
        var response = await _httpClient.GetAsync($"/Text/Pop/{id}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync() ?? string.Empty;

        return string.Empty;
    }

    /// <summary>
    /// Stores text with an id on the server. Appends if id already contains text.
    /// </summary>
    /// <param name="entry">Text entry to store on the server containing the id and text.</param>
    /// <returns>Server response to push.</returns>
    public async Task<string> Push(TextEntry entry)
    {
        var response = await _httpClient.PostAsync($"/Text/Push", new StringContent(JsonSerializer.Serialize(entry), System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync() ?? string.Empty;

        return $"Error Response: '{response.StatusCode}'";
    }
}
