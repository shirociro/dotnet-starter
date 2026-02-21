using netcore.Data;
using Microsoft.EntityFrameworkCore;

namespace netcore.Modules.Tasks
{
    public class TaskService
    {
        private readonly AppDbContext _context;

        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskModel>> GetAllAsync()
        {
            return await _context.Tasks.Include(t => t.User).ToListAsync();
        }

        public async Task<TaskModel> CreateAsync(TaskCreateDto dto)
        {
            var task = new TaskModel
            {
                Title = dto.Title,
                UserId = dto.UserId
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }
    }
}