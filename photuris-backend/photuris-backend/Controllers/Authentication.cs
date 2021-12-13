using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using photuris_backend.DbContext;
using photuris_backend.DbContext.Entities;
using photuris_backend.DTOs;
using photuris_backend.Extensions;

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


        [HttpPost("Login")]
        public IActionResult Login([FromBody]LoginDto loginData)
        {
            _repository.Users.Add(new User() { Email = loginData.Email, Password = loginData.Password });
            _repository.SaveChanges();
            return Ok();
        }
    }
}
