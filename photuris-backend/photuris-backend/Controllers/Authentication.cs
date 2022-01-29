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

        [HttpGet("CheckIsLoggedIn")]
        public async Task<bool> IsLoggedIn(string token)
        {
            if (string.IsNullOrEmpty(token))
                return false;

            return await _repository.Sessions.FirstOrDefaultAsync(s => s.Token == token) != null;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]UserDataDto loginData)
        {
            if (loginData.Password.IsNullOrEmpty() || loginData.Email.IsNullOrEmpty())
                return StatusCode(StatusCodes.Status401Unauthorized, "invalid credentials.");

            try
            {
                if (!string.IsNullOrEmpty(loginData.SessionToken))
                {
                    var session = _repository.Sessions.FirstOrDefault(s => s.Token == loginData.SessionToken);
                    if (session != null)
                        return Ok(new AuthenticationCookie
                            {Token = loginData.SessionToken, ExpirationDate = session.ExpirationDate});
                }

                var user = _repository.Users.FirstOrDefault(u => u.Email == loginData.Email);
                if (user == null) return StatusCode(StatusCodes.Status401Unauthorized, "user not found.");

                var hashedAndSaltedInputPassword = (user.PasswordSalt + loginData.Password).ApplySha256();
                if (hashedAndSaltedInputPassword != user.Password)
                    return StatusCode(StatusCodes.Status401Unauthorized, "invalid password.");

                var sessionToken = Guid.NewGuid().ToString();
                var sessionExpirationDate = DateTime.Now.AddHours(3);
                _repository.Sessions.Add(new Session
                    {Token = sessionToken, UserId = user.Id, ExpirationDate = sessionExpirationDate});
                await _repository.SaveChangesAsync();

                return Ok(new AuthenticationCookie {Token = sessionToken, ExpirationDate = sessionExpirationDate});
            }
            catch (Exception e)
            {
                return this.InternalServerError("bad things happened: " + e.Message);
            }
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDataDto registerData)
        {
            if(registerData.Email.IsNullOrEmpty() || registerData.Password.IsNullOrEmpty()) return StatusCode(StatusCodes.Status401Unauthorized, "empty password or email.");

            var salt = StringExtensions.GetRandom(5);
            var saltedAndHashedPassword = (salt + registerData.Password).ApplySha256();

            try
            {
                _repository.Users.Add(new User
                {
                    Email = registerData.Email,
                    Password = saltedAndHashedPassword,
                    PasswordSalt = salt,
                    HashAlgorithm = HashAlgorithmName.SHA256.ToString()
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
