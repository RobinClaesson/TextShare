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
        if (!_fileService.FileExists(id))
            return NotFound($"No text is stored with id '{id}'");

        return Ok(_fileService.GetText(id));
    }


    [HttpPost("{id}/{value}")]
    public IActionResult Post(string id, string value)
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
