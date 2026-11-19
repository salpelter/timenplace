namespace TimeNPlace.Models.Geosearch.DTOs;

public class GeosearchDto
{
    public string ImageUrl { get; set; }
    public string? Author { get; set; }
    public string FileName { get; set; }
    public List<CoordinatesDto> Coordinates { get; set; }
}