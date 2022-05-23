using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;
using photuris_backend.DTOs;
using photuris_backend.ViewModels;

namespace photuris_backend.DbContext.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public User User { get; set; }

        private ICollection<Picture> _pictures;
        public virtual ICollection<Picture> Pictures
        {
            get => _pictures ?? (_pictures = new Collection<Picture>());
            set => _pictures = value;
        }

        public AlbumViewModel FromAlbumEntity()
        {
            return new AlbumViewModel()
            {
                Id = this.Id,
                Name = this.Name,
            };
        }
    }
}
