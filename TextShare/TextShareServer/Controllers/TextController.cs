using Microsoft.AspNetCore.Mvc;
using TextShareServer.Services;

namespace TextShareServer.Controllers;

[Route("api/")]
[ApiController]
public class TextController : ControllerBase
{
    private FileService _fileService;

    public TextController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        try
        {
            var text = _fileService.GetText(id);

            if (text == string.Empty)
                return NotFound();

            return Ok(text);
        }
        catch
        {
            return new ObjectResult(StatusCodes.Status500InternalServerError);
        }
    }

    [HttpPost("{id}/{value}")]
    public IActionResult Post(string id, string value)
    {
        try
        {
            _fileService.StoreText(id, value);
        }
        catch
        {
            return new ObjectResult(StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }
}
