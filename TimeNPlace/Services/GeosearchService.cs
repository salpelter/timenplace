using System.Globalization;
using System.Text.Json;
using TimeNPlace.Models.Geosearch.DTOs;
using TimeNPlace.Models.Geosearch.Response;

namespace TimeNPlace.Services;

public class GeosearchService(HttpClient http)
{
    public async Task<List<GeosearchDto>> GetGeosearchResultsAsync(double lat, double lng)
    {
        // TODO: consider using Wikimedia Dumps if possible
        // TODO: avoid using Action API if applicable
        var query = "?action=query&generator=geosearch"
                    + "&ggsprimary=all&ggsnamespace=6&ggsradius=200" // TODO: provide an option to adjust radius
                    + $"&ggscoord={lat}|{lng}&ggslimit=10" // TODO: add pagination
                    + "&prop=coordinates%7Cimageinfo"
                    + "&iiprop=url%7Cmime%7Cextmetadata%7Ctimestamp%7Cuser"
                    + "&format=json&origin=*";
        
        var json = await http.GetAsync(query);
        
        var response = await json.Content.ReadAsStringAsync();
        var geosearchResponse = JsonSerializer.Deserialize<GeosearchResponse>(response);
        var dtos = new List<GeosearchDto>();

        foreach (var result in geosearchResponse.Query.Pages.Values)
        {
            // TODO: filter out audio files (e.g. ".ogv")
            // TODO: should i keep videos?
            if (result.PageTitle.Contains("Mapillary"))
            {
                // crowdsourced geotagged photos including 360 and street imagery,
                // excluded because looks weird (at least at the moment)
                continue;
            }
            
            var imageinfo = result.ImageInfo.First();
            var coordinates = result.Coordinates?.FirstOrDefault();

            if (coordinates is null &&
                imageinfo.ExtMetaData.TryGetValue("GPSLatitude", out var latitudeMetadata) &&
                imageinfo.ExtMetaData.TryGetValue("GPSLongitude", out var longitudeMetadata) &&
                double.TryParse(latitudeMetadata.Value.GetString(), CultureInfo.InvariantCulture, out var latitude) &&
                double.TryParse(longitudeMetadata.Value.GetString(), CultureInfo.InvariantCulture, out var longitude))
            {
                coordinates = new Coordinates
                {
                    Latitude = latitude,
                    Longitude = longitude
                };
            }
            
            var dotIndex = result.PageTitle.LastIndexOf('.');
            
            var dto = new GeosearchDto()
            {
                ImageUrl = imageinfo.Url,
                FileName = result.PageTitle
                    .Remove(dotIndex, result.PageTitle.Length - dotIndex) // removes file extension
                    .Substring(5), // removes leading "File:"
                Author = imageinfo.User,
                Coordinates =
                [
                    new CoordinatesDto
                    {
                        Latitude = coordinates.Latitude,
                        Longitude = coordinates.Longitude
                    }
                ]
            };
            
            dtos.Add(dto);
        }

        return dtos;
    }
}