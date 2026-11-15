using System.Text.Json;
using TimeNPlace.Models;

namespace TimeNPlace.Services;

public class GeosearchService(HttpClient http)
{
    public async Task<string> GetFirstGeosearchImageUrlAsync(double lat, double lng)
    {
        // TODO: do i need to add Accept-Encoding: gzip?
        http.DefaultRequestHeaders.Add("User-Agent", "time-n-place-crawler-bot/0.1 (User: amusable.wick197@passfwd.com)");

        // TODO: consider using Wikimedia Dumps if possible
        // TODO: avoid using Action API if applicable
        var url = "https://commons.wikimedia.org/w/api.php"
                  + "?action=query&generator=geosearch"
                  + "&ggsprimary=all&ggsnamespace=6&ggsradius=1000" // TODO: adjust radius (show pictures based on given radius?)
                  + $"&ggscoord={lat}|{lng}&ggslimit=10" // TODO: adjust limit (pagination?)
                  + "&prop=coordinates%7Cimageinfo"
                  + "&iiprop=url%7Cmime%7Cextmetadata%7Ctimestamp%7Cuser"
                  + "&format=json&origin=*";
        
        var json = await http.GetAsync(url);
        
        var response = await json.Content.ReadAsStringAsync();
        var geosearchResponse = JsonSerializer.Deserialize<GeosearchResponse>(response);

        var imageUrl = geosearchResponse.Query.Pages.Values
            .OrderBy(p => p.Index)
            .Select(p => p.ImageInfo.First().Url)
            .First();

        return imageUrl;
    }
}