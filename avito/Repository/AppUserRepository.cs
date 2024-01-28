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
        private readonly UserManager<AppUser> _userManager;
        public AppUserRepository(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        async public Task<List<AppUser>> GetAllAppUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetAppUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<bool> CreateAppUser(string password, AppUser user) {
            var result = await _userManager.CreateAsync(new AppUser() { UserName = user.UserName, Email = user.Email }, password);
            return result.Succeeded;
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

        public async Task<bool> AppUserExists(string id)
        {
            var result = await _userManager.FindByIdAsync(id);
            return result!=null;
        }
        public async Task<bool> DeleteAppUser(AppUser appUser)
        {
            var result = await _userManager.DeleteAsync(appUser);
            return result.Succeeded;
        }
    }
}
