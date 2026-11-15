using System.Text.Json.Serialization;

namespace TimeNPlace.Models.Geosearch;

public class Query
{
    [JsonPropertyName("pages")]
    public Dictionary<string, PageWithImage> Pages { get; set; }
}