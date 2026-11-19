using Microsoft.AspNetCore.Mvc;
using TimeNPlace.Models.Geosearch.DTOs;
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
    public async Task<ActionResult<List<GeosearchDto>>> GetGeosearchResultsAsync(double lat, double lng)
    {
        // TODO: do i need to wrap degrees?
        var result = await _service.GetGeosearchResultsAsync(lat, lng);
        // TODO: error handling here or in the service?
        return Ok(result);
    }
}