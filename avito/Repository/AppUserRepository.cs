using avito.Data;
using avito.Interfaces;
using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Repository
{
    public class AppUserRepository: IAppUserRepository
    {
        private readonly AppDbContext _context;
        public AppUserRepository(AppDbContext context) {
            _context = context;
        }

        async public Task<List<AppUser>> GetAllAppUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetAppUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
    }
}
