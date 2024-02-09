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

        var text = _fileService.GetText(id);

        if (text == string.Empty)
            return NotFound();

        return Ok(text);
    }

    [HttpPost("{id}/{value}")]
    public IActionResult Post(string id, string value)
    {
        _fileService.StoreText(id, value);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        if (!_fileService.FileExists(id))
            return NotFound();

        _fileService.DeleteText(id);
        return Ok();
    }
}
