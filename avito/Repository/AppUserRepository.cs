using avito.Data;
using avito.Interfaces;
using avito.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace avito.Repository
{
    public class AppUserRepository: IAppUserRepository
    {
        private readonly AppDbContext _context;
        public AppUserRepository(AppDbContext context)
        {
            _context = context;
        }

        async public Task<List<AppUser>> GetAllAppUsers()
        {
            return await _context.AppUsers.ToListAsync();
        }

        public async Task<AppUser> GetAppUserById(int id)
        {
            return await _context.AppUsers.FindAsync(id);
        }

        public bool CreateAppUser(AppUser user) {
            _context.Add(user);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(AppUser user)
        {
            _context.AppUsers.Update(user);
            return Save();
        }

        public bool AppUserExists(int id)
        {
            return _context.AppUsers.Any(p=>p.Id==id);
        }
        public bool DeleteAppUser(AppUser appUser)
        {

            _context.Remove(appUser);
            return Save();
        }

        public bool UpdateAppUser(AppUser appUser)
        {
            _context.Update(appUser);
            return Save();
        }
    }
}
