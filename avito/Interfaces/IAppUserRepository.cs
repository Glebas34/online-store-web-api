using avito.Models;
using Microsoft.EntityFrameworkCore;

namespace avito.Interfaces
{
    public interface IAppUserRepository
    {
        public Task<List<AppUser>> GetAllAppUsers();

        public Task<AppUser> GetAppUserById(string id);

        public bool Save();


        public bool Update(AppUser user);

    }
}
}
