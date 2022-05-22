using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace photuris_backend.DbContext.Entities
{
    public class Album
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public User User { get; set; }
        public virtual ICollection<Picture> Pictures { get; set; }
    }
}
