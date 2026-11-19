using System.Text.Json.Serialization;

namespace TimeNPlace.Models.Geosearch.Response;

public class PageWithImage
{
    [JsonPropertyName("pageid")]
    public int PageId { get; set; }

    [JsonPropertyName("ns")]
    public int Ns { get; set; } // namespace: 6 (files only - images and media)

    [JsonPropertyName("title")]
    public string PageTitle { get; set; }

    [JsonPropertyName("index")]
    public int Index { get; set; }

    [JsonPropertyName("coordinates")]
    public List<Coordinates>? Coordinates { get; set; }

    [JsonPropertyName("imagerepository")]
    public string ImageRepository { get; set; }

    [JsonPropertyName("imageinfo")]
    public List<ImageInfo> ImageInfo { get; set; }
}