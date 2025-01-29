using MediaTracker.Model.entities;
using Microsoft.AspNetCore.Mvc;

namespace MediaTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaTypeController : ControllerBase
{
    private readonly MediaTypeRepository _repository;

    public MediaTypeController(MediaTypeRepository repository)
    {
        _repository = repository;
    }

    // Get a media type by ID
    [HttpGet("{id}")]
    public ActionResult<MediaType> GetMediaType(int id)
    {
        var mediaType = _repository.GetMediaTypeById(id);
        if (mediaType == null)
        {
            return NotFound($"Media type with ID {id} not found.");
        }
        return Ok(mediaType);
    }

    // Get all media types
    [HttpGet]
    public ActionResult<IEnumerable<MediaType>> GetMediaTypes()
    {
        var mediaTypes = _repository.GetMediaTypes();
        return Ok(mediaTypes);
    }

    // Add a new media type
    [HttpPost]
    public ActionResult CreateMediaType([FromBody] MediaType mediaType)
    {
        if (mediaType == null)
        {
            return BadRequest("Media type information is required.");
        }

        var status = _repository.InsertMediaType(mediaType);
        if (status)
        {
            return CreatedAtAction(nameof(GetMediaType), new { id = mediaType.MediaTypeId }, mediaType);
        }

        return BadRequest("Failed to create the media type.");
    }

    // Update an existing media type
    [HttpPut("{id}")]
    public ActionResult UpdateMediaType(int id, [FromBody] MediaType mediaType)
    {
        if (mediaType == null)
        {
            return BadRequest("Media type information is required.");
        }

        if (id != mediaType.MediaTypeId)
        {
            return BadRequest("Mismatched media type ID in the URL and body.");
        }

        var existingMediaType = _repository.GetMediaTypeById(id);
        if (existingMediaType == null)
        {
            return NotFound($"Media type with ID {id} not found.");
        }

        var status = _repository.UpdateMediaType(mediaType);
        if (status)
        {
            return NoContent();
        }

        return BadRequest("Failed to update the media type.");
    }

    // Delete a media type by ID
    [HttpDelete("{id}")]
    public ActionResult DeleteMediaType(int id)
    {
        var existingMediaType = _repository.GetMediaTypeById(id);
        if (existingMediaType == null)
        {
            return NotFound($"Media type with ID {id} not found.");
        }

        var status = _repository.DeleteMediaType(id);
        if (status)
        {
            return NoContent();
        }

        return BadRequest($"Failed to delete the media type with ID {id}.");
    }
}
