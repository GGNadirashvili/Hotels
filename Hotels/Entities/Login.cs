using System.ComponentModel.DataAnnotations;

namespace Hotels.Entities
{
    public class Login
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public bool RememberMe{ get; set; }
    }
}
