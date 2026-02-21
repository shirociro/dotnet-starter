using netcore.Data;
using Microsoft.EntityFrameworkCore;

namespace netcore.Modules.Users
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserModel>> GetAllAsync()
        {
            return await _context.Users.Include(u => u.Tasks).ToListAsync();
        }

        public async Task<UserModel> CreateAsync(UserCreateDto dto)
        {
            var user = new UserModel
            {
                Username = dto.Username,
                Email = dto.Email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}