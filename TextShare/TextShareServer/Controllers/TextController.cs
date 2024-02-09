using Microsoft.AspNetCore.Mvc;
using TextShareServer.Services;

namespace TextShareServer.Controllers;

[Route("Text/[action]/")]
[ApiController]
public class TextController : ControllerBase
{
    private FileService _fileService;

    public TextController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{id}")]
    public IActionResult Peek(string id)
    {
        if (!_fileService.FileExists(id))
            return NotFound($"No text is stored with id '{id}'");

        return Ok(_fileService.GetText(id));
    }

    [HttpGet("{id}")]
    public IActionResult Pop(string id)
    {
        if (!_fileService.FileExists(id))
            return NotFound($"No text is stored with id '{id}'");

        var text = _fileService.GetText(id);
        _fileService.DeleteText(id);

        return Ok();
    }

    [HttpPost("{id}/{value}")]
    public IActionResult Push(string id, string value)
    {
        _fileService.StoreText(id, value);
        return Ok($"Stored text to '{id}'");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        if (!_fileService.FileExists(id))
            return NotFound($"No text is stored with id '{id}'");

        _fileService.DeleteText(id);
        return Ok($"File {id} deleted");
    }
}
