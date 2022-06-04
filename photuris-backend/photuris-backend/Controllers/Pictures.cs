using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using photuris_backend.DbContext;
using photuris_backend.DbContext.Entities;
using photuris_backend.DTOs;
using photuris_backend.Extensions;
using photuris_backend.Extensions.Pagination;
using photuris_backend.Utilities.Shared;

namespace photuris_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pictures : ControllerBase
    {
        private readonly Repository _repository;
        private readonly UsersManager _usersManager;

        public Pictures(Repository repository, UsersManager usersManager)
        {
            _repository = repository;
            _usersManager = usersManager;
        }

        [HttpPost("upload/{sessionToken}")]
        public async Task<IActionResult> Upload(string sessionToken, [FromForm] string fileBase64, [FromForm] PicturesMetaDataDto metaData)
        {
            try
            {
                var session = _repository.Sessions.FirstOrDefault(s => s.Token == sessionToken);
                if (session == null) return Forbid("you are not logged in.");

                var picture = new Picture()
                {
                    Name = metaData.FileName,
                    UserId = session.UserId,
                    ImageDataBase64 = fileBase64,
                    DateCreated = metaData.DateTimeCreated,
                    SizeInBytes = metaData.FileSize
                };

                _repository.Pictures.Add(picture);
                await _repository.SaveChangesAsync();

                return Ok("picture uploaded.");
            }
            catch (Exception e)
            {
                return this.InternalServerError("bad things happened: " + e.Message);
            }
        }

        [HttpGet("pictures/{sessionToken}/{pageNumber:int}")]
        public async Task<IActionResult> GetPicturesForUser(string sessionToken, int pageNumber)
        {
            if (pageNumber < 1) return Forbid("invalid page number.");

            try
            {
                var user = await _usersManager.GetUser(sessionToken);

                var pictures = _repository.Pictures
                    .Include(p => p.Albums)
                    .Where(p => p.UserId == user.Id)
                    .ToPages(pageNumber, 16);

                return Ok(pictures.Contents
                    .Select(p => p.FromPictureEntity())
                    .ToList());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
