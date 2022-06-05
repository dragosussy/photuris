using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photuris_backend.DbContext;
using photuris_backend.DbContext.Entities;
using photuris_backend.DTOs;
using photuris_backend.Utilities;
using photuris_backend.Utilities.Shared;

namespace photuris_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users : ControllerBase
    {
        private readonly Repository _repository;
        private readonly UsersManager _usersManager;

        public Users(Repository repository, UsersManager usersManager)
        {
            _repository = repository;
            _usersManager = usersManager;
        }

        [HttpGet("details/{sessionToken}")]
        public async Task<IActionResult> GetDetails(string sessionToken)
        {
            if (string.IsNullOrEmpty(sessionToken)) return Unauthorized("empty session token.");
            User currentUser;
            try
            {
                currentUser = await _usersManager.GetUser(sessionToken);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok(new UserDetailsDto
            {
                Email = currentUser.Email,
                DateCreated = currentUser.DateCreated.ToString()
            });
        }

        //TODO: instead of changing email directly, send a verification to the current mail and update only after that link is clicked
        [HttpPut("change-email/{sessionToken}")]
        public async Task<IActionResult> ChangeEmail([FromBody] ChangeEmailDto newEmailDto, string sessionToken)
        {
            if (string.IsNullOrEmpty(newEmailDto?.NewEmail)) return Unauthorized("invalid email.");

            User currentUser;
            try
            {
                currentUser = await _usersManager.GetUser(sessionToken);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            currentUser.Email = newEmailDto.NewEmail;

            _repository.Users.Update(currentUser);
            await _repository.SaveChangesAsync();

            return Ok("email changed.");
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser(string sessionToken)
        {
            User user;
            try
            {
                user = await _usersManager.GetUser(sessionToken);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
            return Ok(user);
        }
    }
}
