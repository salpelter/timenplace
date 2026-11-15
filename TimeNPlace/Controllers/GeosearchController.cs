using Microsoft.AspNetCore.Mvc;
using TimeNPlace.Services;

namespace TimeNPlace.Controllers;

public class GeosearchController : Controller
{
    private readonly GeosearchService _service;

    public GeosearchController(GeosearchService service)
    {
        _service = service;
    }
    
    [HttpGet]
    [Route("api/geosearch")]
    public async Task<ActionResult<string>> GetFirstGeosearchImageUrlAsync(double lat, double lng)
    {
        // TODO: wrap degrees
        var result = await _service.GetFirstGeosearchImageUrlAsync(lat, lng);
        return Ok(result);
    }
}