using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.VisualBasic;

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
    }
}
