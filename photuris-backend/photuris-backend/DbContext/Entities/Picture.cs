using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using photuris_backend.DTOs;

namespace photuris_backend.DbContext.Entities
{
    public class Picture : PicturesMetaData
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string ImageDataBase64 { get; set; }

        private ICollection<Album> _albums;
        public virtual ICollection<Album> Albums 
        {
            get => _albums ?? (_albums = new Collection<Album>());
            set => _albums = value;
        }

        public PictureViewModel FromPictureEntity()
        {
            return new PictureViewModel
            {
                Id = this.Id,
                Name = this.Name,
                UserId = this.UserId,
                ImageDataBase64 = this.ImageDataBase64,
                Albums = this.Albums.Select(a => a.FromAlbumEntity()).ToList()
            };
        }
    }

    public class PicturesMetaData
    {
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public ulong SizeInBytes { get; set; }
    }
}
