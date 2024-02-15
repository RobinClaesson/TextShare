using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using TextShareClient.TextSareSettings;
using TextShareCommons.Models;

namespace TextShareClient;

internal class ApiHandler
{
    private HttpClient _httpClient;

    public ApiHandler()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri(SettingsHandler.Settings.BaseAddress) };
    }

    public async Task<string[]> ListIds()
    {
        var response = await _httpClient.GetAsync("/Text/ListIds");

        if (response.IsSuccessStatusCode)
            return JsonSerializer.Deserialize<string[]>(await response.Content.ReadAsStringAsync() ?? string.Empty) ?? new string[0];

        return new string[0];
    }

    public async Task<string> Peek(string id)
    {
        var response = await _httpClient.GetAsync($"/Text/Peek/{id}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync() ?? string.Empty;

        return string.Empty;
    }

    public async Task<string> Pop(string id)
    {
        var response = await _httpClient.GetAsync($"/Text/Pop/{id}");

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync() ?? string.Empty;

        return string.Empty;
    }

    public async Task<string> Push(TextEntry entry)
    {
        var response = await _httpClient.PostAsync($"/Text/Push", new StringContent(JsonSerializer.Serialize(entry), System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
            return await response.Content.ReadAsStringAsync() ?? string.Empty;

        return $"Error Response: '{response.StatusCode}'";
    }
}
