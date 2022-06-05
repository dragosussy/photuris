using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photuris_backend.DbContext;
using photuris_backend.DbContext.Entities;
using photuris_backend.DTOs;
using photuris_backend.Extensions;
using photuris_backend.Utilities;

namespace photuris_backend
{
    [Route("api/[controller]")]
    [ApiController]
    public class Authentication : ControllerBase
    {
        private readonly Repository _repository;
        
        public Authentication(Repository repository)
        {
            _repository = repository;
        }

        [HttpGet("GetMasterKey")]
        public async Task<string> GetEncryptedMasterKey(string email)
        {
            return (await _repository.Users.FirstOrDefaultAsync(u => u.Email == email))?.EncryptedMasterKey ?? string.Empty;
        }

        [HttpGet("CheckIsLoggedIn")]
        public async Task<bool> IsLoggedIn(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            return await _repository.Sessions.FirstOrDefaultAsync(s => s.Token == token) != null;
        }

        [HttpPost("Logout")]
        public async Task<IActionResult> Logout([FromBody] SessionTokenDto sessionDto)
        {
            try
            {
                var session = await _repository.Sessions.FirstOrDefaultAsync(s => s.Token == sessionDto.SessionToken);

                if (session == null) return BadRequest("invalid session.");

                _repository.Sessions.Remove(session);
                await _repository.SaveChangesAsync();
            } catch (Exception e)
            {
                return this.InternalServerError("bad things happened: " + e.Message);
            }

            return Ok("session cookie deleted.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserDataDto loginData)
        {
            if (loginData.Password.IsNullOrEmpty() || loginData.Email.IsNullOrEmpty())
                return Unauthorized("invalid credentials.");

            try
            {
                var user = _repository.Users.FirstOrDefault(u => u.Email == loginData.Email);
                if (user == null) return Unauthorized("user not found.");

                if (!string.IsNullOrEmpty(loginData.SessionToken))
                {
                    var session = _repository.Sessions.FirstOrDefault(s => s.Token == loginData.SessionToken);
                    if (session != null)
                        return Ok(new AuthenticationCookie
                        {
                            Token = loginData.SessionToken, 
                            ExpirationDate = session.ExpirationDate,
                        });
                }

                var hashedAndSaltedInputPassword = PasswordHandler.EncryptPasswordSha256(user.PasswordSalt, loginData.Password);
                if (hashedAndSaltedInputPassword != user.Password) return Unauthorized("invalid password.");

                var sessionToken = Guid.NewGuid().ToString();
                var sessionExpirationDate = DateTime.Now.AddHours(3);
                _repository.Sessions.Add(new Session
                    {Token = sessionToken, UserId = user.Id, ExpirationDate = sessionExpirationDate});
                await _repository.SaveChangesAsync();

                return Ok(new AuthenticationCookie
                {
                    Token = sessionToken, 
                    ExpirationDate = sessionExpirationDate,
                });
            }
            catch (Exception e)
            {
                return this.InternalServerError("bad things happened: " + e.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDataDto registerData)
        {
            if (string.IsNullOrEmpty(registerData.Email) || string.IsNullOrEmpty(registerData.Password) ||
                string.IsNullOrEmpty(registerData.EncryptedMasterKey)) return Unauthorized("empty password or email.");

            var salt = PasswordHandler.GenerateSalt(5);
            var saltedAndHashedPassword = PasswordHandler.EncryptPasswordSha256(salt, registerData.Password);

            var existingUser = await _repository.Users.FirstOrDefaultAsync(u => u.Email == registerData.Email);
            if (existingUser != null) return Unauthorized("user with that email already exists.");

            try
            {
                _repository.Users.Add(new User
                {
                    Email = registerData.Email,
                    Password = saltedAndHashedPassword,
                    PasswordSalt = salt,
                    HashAlgorithm = HashAlgorithmName.SHA256.ToString(),
                    EncryptedMasterKey = registerData.EncryptedMasterKey,
                    DateCreated = DateTime.Now
                });
                await _repository.SaveChangesAsync();

                return Ok("account created");
            }
            catch (Exception e)
            {
                return this.InternalServerError("bad things happened: " + e.Message);
            }
        }
    }
}
