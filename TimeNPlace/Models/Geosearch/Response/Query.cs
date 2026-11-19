using System.Text.Json.Serialization;

namespace TimeNPlace.Models.Geosearch.Response;

public class Query
{
    [JsonPropertyName("pages")]
    public Dictionary<string, PageWithImage> Pages { get; set; }
}