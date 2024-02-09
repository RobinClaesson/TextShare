using Microsoft.AspNetCore.Mvc;
using TextShareServer.Models;
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

    [HttpGet]
    public ActionResult<IEnumerable<string>> ListIds()
    {
        return Ok(_fileService.GetAllId());
    }

    [HttpGet]
    public ActionResult<IEnumerable<TextEntry>> ListEntries()
    {
        return Ok(_fileService.GetAllTextEntries());
    }

    [HttpGet("{id}")]
    public ActionResult<string> Peek(string id)
    {
        if (!_fileService.HasText(id))
            return NotFound($"No text is stored with id '{id}'");

        return Ok(_fileService.GetText(id));
    }

    [HttpGet("{id}")]
    public ActionResult<string> Pop(string id)
    {
        if (!_fileService.HasText(id))
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
        if (!_fileService.HasText(id))
            return NotFound($"No text is stored with id '{id}'");

        _fileService.DeleteText(id);
        return Ok($"File {id} deleted");
    }
}
