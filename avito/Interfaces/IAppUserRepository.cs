using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Interfaces
{
    public interface IAppUserRepository
    {
        public Task<List<AppUser>> GetAllAppUsers();

        public Task<AppUser> GetAppUserById(int id);

        public bool Save();

        public bool CreateAppUser(AppUser user);

        public bool Update(AppUser user);
        public bool AppUserExists(int id);
        public bool DeleteAppUser(AppUser appUser);
        public bool UpdateAppUser(AppUser appUser);
        

    }
}
