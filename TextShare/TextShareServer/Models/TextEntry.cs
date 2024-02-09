﻿namespace TextShareServer.Models;

public record TextEntry
{
    public string Id { get; set; } = string.Empty;
    public string Text { get; set; } = string.Empty;
}
