using Microsoft.EntityFrameworkCore;
using netcore.Modules.Users;
using netcore.Modules.Tasks;

namespace netcore.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }
    }
}