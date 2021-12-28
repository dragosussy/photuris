using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;

namespace photuris_backend.DbContext.Entities
{
    public class Picture
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Blob BinaryImageData { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public ulong SizeInBytes { get; set; }
    }
}
