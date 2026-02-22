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
            return await _context.Tasks
                .Include(t => t.User)
                .OrderByDescending(t => t.CreatedAt) 
                .ToListAsync();
        }

        public async Task<TaskModel> CreateAsync(TaskCreateDto dto)
        {
            var task = new TaskModel
            {
                Title = dto.Title,
                IsCompleted = dto.IsCompleted,
                UserId = dto.UserId

            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<TaskModel?> UpdateAsync(Guid id, TaskCreateDto dto)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return null;

            task.Title = dto.Title;
            task.IsCompleted = dto.IsCompleted;
            task.UserId = dto.UserId;

            await _context.SaveChangesAsync();
            return task;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var task = await _context.Tasks.FindAsync(id);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}