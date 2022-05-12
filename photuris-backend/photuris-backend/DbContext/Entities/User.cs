using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;

namespace photuris_backend.DbContext.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                           + "@"
                           + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{7,}$")]
        public string Password { get; set; }

        [AllowNull]
        public string PasswordSalt { get; set; }

        [Required]
        public string HashAlgorithm { get; set; }
        
        [Required]
        public string EncryptedMasterKey { get; set; }
    }
}
