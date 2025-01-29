using MediaTracker.Model.entities;
using MediaTracker.Model.repositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaCategoryController : ControllerBase
{
       private readonly MediaCategoryRepository _repository;

    public MediaCategoryController(MediaCategoryRepository repository)
    {
        _repository = repository;
    }

    // Get a category by ID
    [HttpGet("{id}")]
    public ActionResult<MediaCategory> GetMediaCategory(int id)
    {
        var category = _repository.GetMediaCategoryById(id);
        if (category == null)
        {
            return NotFound($"Media category with ID {id} not found.");
        }
        return Ok(category);
    }

    // Get all media categories
    [HttpGet]
    public ActionResult<IEnumerable<MediaCategory>> GetMediaCategories()
    {
        var categories = _repository.GetMediaCategories();
        return Ok(categories);
    }

    // Add a new media category
    [HttpPost]
    public ActionResult CreateMediaCategory([FromBody] MediaCategory category)
    {
        if (category == null)
        {
            return BadRequest("Media category information is required.");
        }

        var status = _repository.InsertMediaCategory(category);
        if (status)
        {
            return CreatedAtAction(nameof(GetMediaCategory), new { id = category.CategoryId }, category);
        }

        return BadRequest("Failed to create the media category.");
    }

    // Update an existing media category
    [HttpPut("{id}")]
    public ActionResult UpdateMediaCategory(int id, [FromBody] MediaCategory category)
    {
        if (category == null)
        {
            return BadRequest("Media category information is required.");
        }

        if (id != category.CategoryId)
        {
            return BadRequest("Mismatched category ID in the URL and body.");
        }

        var existingCategory = _repository.GetMediaCategoryById(id);
        if (existingCategory == null)
        {
            return NotFound($"Media category with ID {id} not found.");
        }

        var status = _repository.UpdateMediaCategory(category);
        if (status)
        {
            return NoContent();
        }

        return BadRequest("Failed to update the media category.");
    }

    // Delete a media category by ID
    [HttpDelete("{id}")]
    public ActionResult DeleteMediaCategory(int id)
    {
        var existingCategory = _repository.GetMediaCategoryById(id);
        if (existingCategory == null)
        {
            return NotFound($"Media category with ID {id} not found.");
        }

        var status = _repository.DeleteMediaCategory(id);
        if (status)
        {
            return NoContent();
        }

        return BadRequest($"Failed to delete the media category with ID {id}.");
    }
}
