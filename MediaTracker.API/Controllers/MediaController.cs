using MediaTracker.Model.entities;
using MediaTracker.Model.repositories;
using Microsoft.AspNetCore.Mvc;
namespace MediaTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MediaController : ControllerBase
    {
        private readonly MediaRepository _repository;

        public MediaController(MediaRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public ActionResult<Media> GetMedia([FromRoute] int id)
        {
            var media = _repository.GetMediaById(id);
            if (media == null)
                return NotFound();

            return Ok(media);
        }
 
        [HttpGet]
        public ActionResult<IEnumerable<Media>> GetMediaItems()
        {
            return Ok(_repository.GetMediaItems());
        }

        [HttpPost]
        public ActionResult CreateMedia([FromBody] Media media)
        {
            if (media == null)
                return BadRequest("Media information is missing");

            var status = _repository.InsertMedia(media);
            if (status)
                return Ok();

            return BadRequest("Failed to create media");
        }

        [HttpPut("{id}")]
        public ActionResult UpdateMedia(int id, [FromBody] Media media)
        {
            if (media == null)
                return BadRequest("Media information is missing");

            if (id != media.Id)
                return BadRequest("ID in URL and body do not match");

            var existingMedia = _repository.GetMediaById(id);
            if (existingMedia == null)
                return NotFound($"Media with ID {id} not found");

            var status = _repository.UpdateMedia(media);
            if (status)
                return Ok();

            return BadRequest("Failed to update media");
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteMedia([FromRoute] int id)
        {
            var existingMedia = _repository.GetMediaById(id);
            if (existingMedia == null)
                return NotFound($"Media with ID {id} not found");

            var status = _repository.DeleteMedia(id);
            if (status)
                return NoContent();

            return BadRequest($"Failed to delete media with ID {id}");
        }
}
