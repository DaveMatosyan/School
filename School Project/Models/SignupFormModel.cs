
using System.ComponentModel.DataAnnotations;

namespace School_Project.Models
{
    public class SignupFormModel
    {
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string? ClassId { get; set; } = null!;
        public string? Profession { get; set; } = null!;

    }
}
