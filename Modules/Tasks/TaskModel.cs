using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization; // Add this!
using netcore.Modules.Users;

namespace netcore.Modules.Tasks
{
    public class TaskModel
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Title { get; set; } = string.Empty;

        public bool IsCompleted { get; set; } = false;

        [ForeignKey("User")]
        public Guid UserId { get; set; }

        // The Fix: This stops the serializer from trying to include the 
        // full User object, which prevents the cycle: Task -> User -> Tasks...
        [JsonIgnore] 
        public UserModel? User { get; set; } 
    }
}