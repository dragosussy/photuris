using photuris_backend.DbContext.Entities;
using photuris_backend.ViewModels;

namespace photuris_backend.DTOs
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string ImageDataBase64 { get; set; }
        public DateTime DateCreated { get; set; }
        public List<AlbumViewModel> Albums { get; set; }
    }
}
