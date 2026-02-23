using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization; // Required for JsonIgnore
using netcore.Modules.Tasks;

namespace netcore.Modules.Users
{
    public class UserModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(50)] // Good for Postgres performance
        public string Username { get; set; } = string.Empty;

        [DataType(DataType.Password)] // Tells UI/ORM this is a password field
        public string? Password { get; set; } 

        
        // [EmailAddress] // Adds automatic validation
        // public string Email { get; set; } = string.Empty;
       
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // [JsonIgnore] 
        public List<TaskModel>? Tasks { get; set; } = new(); 
    }
}