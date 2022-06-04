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
    public class Albums : ControllerBase
    {
        private readonly Repository _repository;
        private readonly UsersManager _usersManager;

        public Albums(Repository repository, UsersManager usersManager)
        {
            _repository = repository;
            _usersManager = usersManager;
        }

        [HttpGet("AlbumNames/{sessionToken}")]
        public async Task<IActionResult> GetAllAlbumNames(string sessionToken)
        {
            var user = await _usersManager.GetUser(sessionToken);

            return Ok(await _repository.Albums
                .Where(a => a.User.Id == user.Id)
                .Select(a => new {Id = a.Id, Name = a.Name})
                .ToListAsync());
        }

        [HttpGet("{sessionToken}/{albumName}/{pageNumber:int}")]
        public async Task<IActionResult> GetAlbumPictures(string sessionToken, string albumName, int pageNumber)
        {
            if (pageNumber < 1) return Forbid("invalid page number.");

            try
            {
                var user = await _usersManager.GetUser(sessionToken);
                var pictures = _repository.Albums
                    .Include(a => a.Pictures)
                    .FirstOrDefault(a => a.User.Id == user.Id && a.Name == albumName)
                    ?.Pictures
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

        [HttpPost("Add")]
        public async Task<IActionResult> AddAlbum([FromBody] AlbumDto albumDto)
        {
            try
            {
                var user = await _usersManager.GetUser(albumDto.SessionToken);

                _repository.Albums.Add(new Album()
                {
                    Name = albumDto.AlbumName,
                    User = user
                });
                await _repository.SaveChangesAsync();

                return Ok("album added.");
            }
            catch (Exception e)
            {
                return this.InternalServerError("bad things happened: " + e.Message);
            }
        }

        [HttpPost("AddToAlbum")]
        public async Task<IActionResult> AddPictureToAlbum([FromBody] PictureAlbumDto pictureAlbumDto)
        {
            try
            {
                var user = await _usersManager.GetUser(pictureAlbumDto.SessionToken);

                var album = await _repository.Albums.FirstOrDefaultAsync(a =>
                    a.Name == pictureAlbumDto.AlbumName && a.User.Id == user.Id);
                if (album == null) return BadRequest("invalid album.");

                var picture = await _repository.Pictures.FirstOrDefaultAsync(p => p.Id == pictureAlbumDto.PictureId);
                if (picture == null) return BadRequest("invalid picture.");

                album.Pictures.Add(picture);
                await _repository.SaveChangesAsync();

                return Ok("picture added to album.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
