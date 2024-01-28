using AutoMapper;
using avito.Dto;
using avito.Interfaces;
using avito.Models;
using avito.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        public AppUserController(IAppUserRepository appUserRepository, IProductRepository productRepository,
            SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IMapper mapper) {
            _appUserRepository = appUserRepository;
            _productRepository = productRepository;
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(AppUser))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAppUser(string userId) {
            if (!_appUserRepository.UserExists(userId)) {
                NotFound();
            }
            var user = await _appUserRepository.GetAppUserById(userId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppUser>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAppUsers()
        {
            var users = await _appUserRepository.GetAllAppUsers();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpPost("{appUserCreate}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAppUser([FromQuery]string password,[FromBody] AppUserDto appUserDto)
        {
            if (appUserDto == null)
            {
                return BadRequest(ModelState);
            }
            if (await _userManager.FindByEmailAsync(appUserDto.Email)!=null)
            {
                ModelState.AddModelError("", "Пользователь с такой почтой уже существует");
                return StatusCode(422, ModelState);
            }
            var appUserMap = _mapper.Map<AppUserDto>(appUserDto);
            var result = await _userManager.CreateAsync(new AppUser() { UserName = appUserMap.UserName, Email = appUserMap.Email }, password);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }



    }
}
