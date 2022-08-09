namespace School_Project.Models
{
    public class SignupFormModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string? ClassId { get; set; } = null!;
        public string? Profession { get; set; } = null!;

    }
}
