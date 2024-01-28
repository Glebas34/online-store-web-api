using AutoMapper;
using avito.Dto;
using avito.Models;

namespace avito.Helper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<AppUser, AppUserDto>();
        }
    }
}
