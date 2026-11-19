using System.Text.Json.Serialization;

namespace TimeNPlace.Models.Geosearch.Response;

public class Coordinates
{
    [JsonPropertyName("lat")]
    public double Latitude { get; set; }

    [JsonPropertyName("lon")]
    public double Longitude { get; set; }

    [JsonPropertyName("primary")]
    public string Primary { get; set; }

    [JsonPropertyName("globe")]
    public string Globe { get; set; }
}