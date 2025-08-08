using EffectiveMobile.TestWork.API.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EffectiveMobile.TestWork.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdvertisingSpacesController(IDataService dataService) : ControllerBase
{
    // GET api/AdvertisingSpaces?location=/ru/svrd
    [HttpGet]
    public ActionResult<IEnumerable<string>> Get([FromQuery] string location)
    {
        var result = dataService.GetAdvertisingSpaceNames(location);

        return Ok(result);
    }

    // POST api/AdvertisingSpaces/upload
    [HttpPost("upload")]
    public async Task<IActionResult> Upload([FromBody] string filePath)
    {
        var success = await dataService.LoadAdvertisingSpacesAsync(filePath);

        return success ? Ok("File loaded successfully") : BadRequest("Failed to load file");
    }
}
