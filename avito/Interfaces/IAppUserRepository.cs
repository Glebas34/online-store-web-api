using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Interfaces
{
    public interface IAppUserRepository
    {
        public Task<List<AppUser>> GetAllAppUsers();

        public Task<AppUser> GetAppUserById(string id);

        public bool Save();

        public Task<bool> CreateAppUser(string password, AppUser user);

        public bool Update(AppUser user);
        public Task<bool> AppUserExists(string id);
        public Task<bool> DeleteAppUser(AppUser appUser);
        

    }
}
