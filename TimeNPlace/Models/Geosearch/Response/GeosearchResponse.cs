using System.Text.Json.Serialization;

namespace TimeNPlace.Models.Geosearch.Response;

public class GeosearchResponse
{
    [JsonPropertyName("batchcomplete")]
    public string BatchComplete { get; set; }
    
    [JsonPropertyName("query")]
    public Query Query { get; set; }
}