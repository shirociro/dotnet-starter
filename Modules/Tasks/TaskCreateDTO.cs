using System.ComponentModel.DataAnnotations;

namespace netcore.Modules.Tasks
{
    public class TaskCreateDto
    {
        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public Guid UserId { get; set; }
    }
}