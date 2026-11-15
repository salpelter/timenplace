using System.Text.Json.Serialization;
using TimeNPlace.Models.Geosearch;

namespace TimeNPlace.Models; // TODO: restructure model folders

public class GeosearchResponse
{
    [JsonPropertyName("batchcomplete")]
    public string BatchComplete { get; set; }
    
    [JsonPropertyName("query")]
    public Query Query { get; set; }
}