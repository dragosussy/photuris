using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using photuris_backend.DbContext;
using photuris_backend.DbContext.Entities;
using photuris_backend.DTOs;
using photuris_backend.Extensions;

namespace photuris_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Pictures : ControllerBase
    {
        private readonly Repository _repository;

        public Pictures(Repository repository)
        {
            _repository = repository;
        }

        [HttpPost("Upload")]
        public async Task<IActionResult> Upload([FromForm] string sessionToken, [FromForm] IFormFile file, [FromForm] PicturesMetaDataDto metaData)
        {
            try
            {
                var session = _repository.Sessions.FirstOrDefault(s => s.Token == sessionToken);
                if (session == null) return StatusCode(StatusCodes.Status403Forbidden, "you are not logged in.");

                await using var memoryStream = new MemoryStream();
                await file.CopyToAsync(memoryStream);
                var picture = new Picture()
                {
                    Name = file.FileName,
                    UserId = session.UserId,
                    BinaryImageData = memoryStream.ToArray(),
                    DateCreated = metaData.DateCreated,
                    SizeInBytes = metaData.SizeInBytes
                };

                _repository.Add(picture);
                await _repository.SaveChangesAsync();

                return Ok();
            }
            catch (Exception e)
            {
                return this.InternalServerError("bad things happened: " + e.Message);
            }
        }
    }
}
