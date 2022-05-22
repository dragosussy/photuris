using photuris_backend.DbContext.Entities;

namespace photuris_backend.DTOs
{
    public class PictureViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public string ImageDataBase64 { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
