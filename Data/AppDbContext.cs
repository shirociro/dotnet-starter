using Microsoft.EntityFrameworkCore;
using netcore.Modules.Users;
using netcore.Modules.Tasks;

// namespace netcore.Data
// {
//     public class AppDbContext : DbContext
//     {
//         public AppDbContext(DbContextOptions<AppDbContext> options)
//             : base(options) { }

//         public DbSet<UserModel> Users { get; set; }
//         public DbSet<TaskModel> Tasks { get; set; }
//     }
// }

namespace netcore.Data  
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Force exact table names to match your Controller/Service logic
            modelBuilder.Entity<UserModel>().ToTable("Users");
            modelBuilder.Entity<TaskModel>().ToTable("Tasks");
        }
    }
}