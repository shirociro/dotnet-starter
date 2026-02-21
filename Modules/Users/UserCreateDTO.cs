using System.ComponentModel.DataAnnotations;

namespace netcore.Modules.Users
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    
    }
}