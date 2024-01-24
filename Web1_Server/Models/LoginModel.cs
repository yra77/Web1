

using System.ComponentModel.DataAnnotations;


namespace Web1_Server.Models
{
    public class LoginModel
    {
        public Guid Id { get; set; }

        [Required]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}
