using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        public string BinaryImageData { get; set; }
    }

    public class PicturesMetaData
    {
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public ulong SizeInBytes { get; set; }
    }
}
