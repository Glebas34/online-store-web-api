using AutoMapper;
using avito.Dto;
using avito.Interfaces;
using avito.Models;
using Microsoft.AspNetCore.Mvc;

namespace avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUserController : ControllerBase
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        public AppUserController(IAppUserRepository appUserRepository, IShoppingCartRepository shoppingCartRepository, IMapper mapper) {
            _appUserRepository = appUserRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }

        [HttpGet("{appUserId:int}")]
        [ProducesResponseType(200, Type = typeof(AppUserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAppUser(int appUserId) {
            if (!_appUserRepository.AppUserExists(appUserId)) {
                NotFound();
            }
            var user = _mapper.Map<AppUserDto>(await _appUserRepository.GetAppUserById(appUserId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(user);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AppUserDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAppUsers()
        {
            var users = _mapper.Map<List<AppUserDto>>(await _appUserRepository.GetAllAppUsers());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(users);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAppUser([FromBody] AppUserDto appUserDto)
        {
            if (appUserDto == null)
            {
                return BadRequest(ModelState);
            }
            if (_appUserRepository.AppUserExists(appUserDto.Id))
            {
                ModelState.AddModelError("", "Пользователь с такой почтой уже существует");
                return StatusCode(422, ModelState);
            }
            var appUserMap = _mapper.Map<AppUser>(appUserDto);
            if (!_appUserRepository.CreateAppUser(appUserMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{appUserId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateProduct(int appUserId, [FromBody] AppUserDto updatedAppUser)
        {
            if (updatedAppUser == null)
            {
                return BadRequest(ModelState);
            }
            if (appUserId != updatedAppUser.Id)
            {
                return BadRequest(ModelState);
            }
            if (!_appUserRepository.AppUserExists(appUserId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var appUserMap = _mapper.Map<AppUser>(updatedAppUser);
            if (!_appUserRepository.UpdateAppUser(appUserMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно обновлено");
        }

        [HttpDelete("{appUserId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteAppUser(int appUserId)
        {
            if (!_appUserRepository.AppUserExists(appUserId))
            {
                return NotFound();
            }
            var appUserToDelete = await _appUserRepository.GetAppUserById(appUserId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var shoppingCartToDelete = appUserToDelete.ShoppingCart;
            if (shoppingCartToDelete != null)
            {
                if (!_shoppingCartRepository.DeleteShoppingCart(shoppingCartToDelete))
                {
                    ModelState.AddModelError("", "Что-то пошло не так при удалении");
                    return StatusCode(500, ModelState);
                }
            }
            if (!_appUserRepository.DeleteAppUser(appUserToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
