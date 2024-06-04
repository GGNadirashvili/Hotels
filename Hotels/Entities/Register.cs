using System.ComponentModel.DataAnnotations;

namespace Hotels.Entities
{
    public class Register
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]

        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required string Password { get; set; }
        public required int Number { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and confirmation password did not match")]
        public required string ConfirmPassword { get; set; }
    }
}