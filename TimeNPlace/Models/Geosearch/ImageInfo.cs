using System.Text.Json.Serialization;

namespace TimeNPlace.Models.Geosearch;

public class ImageInfo
{
    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; }
    
    [JsonPropertyName("user")]
    public string User { get; set; }
    
    [JsonPropertyName("url")]
    public string Url { get; set; }
    
    [JsonPropertyName("descriptionurl")]
    public string DescriptionUrl { get; set; }
    
    [JsonPropertyName("descriptionshorturl")]
    public string DescriptionShortUrl { get; set; }
    
    [JsonPropertyName("extmetadata")]
    public Dictionary<string, ExtMetaData> ExtMetaData { get; set; }
    
    [JsonPropertyName("mime")]
    public string Mime { get; set; }
}