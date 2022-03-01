using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photuris_backend.DbContext;
using photuris_backend.DbContext.Entities;
using photuris_backend.Utilities;

namespace photuris_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserManager : ControllerBase
    {
        private readonly Repository _repository;

        public UserManager(Repository repository)
        {
            _repository = repository;
        }

        //TODO: instead of changing email directly, send a verification to the current mail and update only after that link is clicked
        [HttpPut("change-email")]
        public async Task<IActionResult> ChangeEmail(string sessionToken, string newEmail)
        {
            User currentUser;
            try
            {
                currentUser = await GetCurrentUserInternal(sessionToken);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            currentUser.Email = newEmail;

            _repository.Users.Update(currentUser);
            await _repository.SaveChangesAsync();

            return Ok("email changed.");
        }

        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword(string sessionToken, string oldPassword, string newPassword, string newPasswordConfirmation)
        {
            if (newPassword != newPasswordConfirmation) return Unauthorized("new passwords don't match.");

            User currentUser;
            try
            {
                currentUser = await GetCurrentUserInternal(sessionToken);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            if (PasswordHandler.EncryptPasswordSha256(currentUser.PasswordSalt, oldPassword) != currentUser.Password)
                return Unauthorized("old password is wrong.");

            currentUser.PasswordSalt = PasswordHandler.GenerateSalt(5);
            currentUser.Password = PasswordHandler.EncryptPasswordSha256(currentUser.PasswordSalt, newPassword);

            _repository.Users.Update(currentUser);
            await _repository.SaveChangesAsync();

            return Ok("password changed.");
        }

        [HttpGet("current-user")]
        public async Task<IActionResult> GetCurrentUser(string sessionToken)
        {
            User user;
            try
            {
                user = await GetCurrentUserInternal(sessionToken);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
            
            return Ok(user);
        }

        private async Task<User> GetCurrentUserInternal(string sessionToken)
        {
            var currentUserSession = await _repository.Sessions.FirstOrDefaultAsync(s => s.Token == sessionToken);
            if (currentUserSession == null) throw new Exception("user session not found.");

            var currentUser = await _repository.Users.FirstOrDefaultAsync(u => u.Id == currentUserSession.UserId);
            if (currentUser == null) throw new Exception("session expired.");

            return currentUser;
        }
    }
}
