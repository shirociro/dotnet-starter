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

        [Required]
        [EmailAddress] // Adds automatic validation
        public string Email { get; set; } = string.Empty;
       
        // [JsonIgnore] 
        public List<TaskModel>? Tasks { get; set; } = new(); 
    }
}