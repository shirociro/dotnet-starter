using System.ComponentModel.DataAnnotations;

namespace netcore.Modules.Tasks
{
    public class TaskCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } 

        [Required]
        public Guid UserId { get; set; }
    }
}