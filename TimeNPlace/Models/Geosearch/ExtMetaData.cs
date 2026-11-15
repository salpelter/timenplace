using System.Text.Json;
using System.Text.Json.Serialization;

namespace TimeNPlace.Models.Geosearch;

public class ExtMetaData
{
    [JsonPropertyName("value")]
    public JsonElement Value { get; set; }

    [JsonPropertyName("source")]
    public string Source { get; set; }

    [JsonPropertyName("hidden")]
    public string? Hidden { get; set; }
}