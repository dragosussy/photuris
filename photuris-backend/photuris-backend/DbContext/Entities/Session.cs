using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace photuris_backend.DbContext.Entities
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
