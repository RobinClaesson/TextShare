using Microsoft.AspNetCore.Mvc;
using TextShareCommons.Models;
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

        return Ok(text);
    }

    [HttpPost()]
    public IActionResult Push([FromBody] TextEntry entry)
    {
        _fileService.StoreText(entry);
        return Ok($"Stored text '{entry.Text}' to '{entry.Id}'");
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(string id)
    {
        if (!_fileService.HasText(id))
            return NotFound($"No text is stored with id '{id}'");

        _fileService.DeleteText(id);
        return Ok($"Text for '{id}' deleted");
    }
}
