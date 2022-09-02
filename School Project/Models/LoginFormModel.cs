using System.ComponentModel.DataAnnotations;

namespace School_Project.Models
{
    public class LoginFormModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
