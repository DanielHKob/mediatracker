using MediaTracker.Model.entities;
using MediaTracker.Model.repositories;
using Microsoft.AspNetCore.Mvc;

namespace MediaTracker.API.Controllers;

[ApiController]
[Route("api/[controller]")]public class UserController : ControllerBase
{

     private readonly UserRepository _repository;

        public UserController(UserRepository repository)
        {
            _repository = repository;
        }

        // Get a user by ID
        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            var user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            return Ok(user);
        }

        // Get all users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            var users = _repository.GetUsers();
            return Ok(users);
        }

        // Add a new user
        [HttpPost]
        public ActionResult CreateUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User information is required.");
            }

            var status = _repository.InsertUser(user);
            if (status)
            {
                return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
            }

            return BadRequest("Failed to create the user.");
        }

        // Update an existing user
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest("User information is required.");
            }

            if (id != user.Id)
            {
                return BadRequest("Mismatched user ID in the URL and body.");
            }

            var existingUser = _repository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var status = _repository.UpdateUser(user);
            if (status)
            {
                return NoContent();
            }

            return BadRequest("Failed to update the user.");
        }

        // Delete a user by ID
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var existingUser = _repository.GetUserById(id);
            if (existingUser == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            var status = _repository.DeleteUser(id);
            if (status)
            {
                return NoContent();
            }

            return BadRequest($"Failed to delete the user with ID {id}.");
        }


}
