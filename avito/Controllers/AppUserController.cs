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
        private readonly IMapper _mapper;
        public AppUserController(IAppUserRepository appUserRepository, IMapper mapper) {
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        [HttpGet("{userId}")]
        [ProducesResponseType(200, Type = typeof(AppUserDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetAppUser(int userId) {
            if (!_appUserRepository.AppUserExists(userId)) {
                NotFound();
            }
            var user = _mapper.Map<AppUserDto>(await _appUserRepository.GetAppUserById(userId));
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

        [HttpPost("{appUserCreate}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateAppUser([FromBody] AppUserDto appUserDto)
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
            if (_appUserRepository.CreateAppUser(appUserMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpDelete("{appUserId}")]
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
            if (!_appUserRepository.DeleteAppUser(appUserToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

    }
}
