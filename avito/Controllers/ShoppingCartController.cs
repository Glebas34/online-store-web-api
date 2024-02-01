using AutoMapper;
using avito.Dto;
using avito.Interfaces;
using avito.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;
        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IAppUserRepository appUserRepository, IMapper mapper)
        {
            _shoppingCartRepository = shoppingCartRepository;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ShoppingCartDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShoppingCart(int id)
        {
            if (_shoppingCartRepository.ShoppingCartExists(id))
            {
                return NotFound();
            }
            var shoppingCart = _mapper.Map<ShoppingCartDto>(await _shoppingCartRepository.GetShoppingCart(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(shoppingCart);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ShoppingCartDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShoppingCarts()
        {
            var shoppingCarts = _mapper.Map<List<ShoppingCartDto>>(await _shoppingCartRepository.GetShoppingCarts());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(shoppingCarts);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetTotalPrice(int id)
        {
            if (!_shoppingCartRepository.ShoppingCartExists(id))
            {
                return NotFound();
            }
            var totalSum = await _shoppingCartRepository.GetTotalPrice(id);
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(totalSum);
        }

        [HttpPost("{shoppingCartCreate}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateShoppingCart([FromQuery] string userId, [FromBody] ProductDto shoppingCartCreate)
        {
            if (shoppingCartCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!_shoppingCartRepository.ShoppingCartExists(shoppingCartCreate.Id))
            {
                ModelState.AddModelError("", "Объявление уже существует");
                return StatusCode(422, ModelState);
            }
            var shoppingCartMap = _mapper.Map<ShoppingCart>(shoppingCartCreate);
            var appUser = await _appUserRepository.GetAppUserById(userId);
            shoppingCartMap.User = appUser;
            if (!_shoppingCartRepository.CreateShoppingCart(shoppingCartMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{shoppingCartId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateShoppingCart(int shoppingCartId, [FromBody] ShoppingCartDto updatedShoppingCart)
        {
            if (updatedShoppingCart == null)
            {
                return BadRequest(ModelState);
            }
            if (shoppingCartId != updatedShoppingCart.Id)
            {
                return BadRequest(ModelState);
            }
            if (_shoppingCartRepository.ShoppingCartExists(shoppingCartId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var shoppingCartMap = _mapper.Map<ShoppingCart>(updatedShoppingCart);
            if (!_shoppingCartRepository.UpdateShoppingCart(shoppingCartMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно обновлено");
        }

        [HttpDelete("{shoppingCartId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteshoppingCart(int shoppingCartId)
        {
            if (!_shoppingCartRepository.ShoppingCartExists(shoppingCartId))
            {
                return NotFound();
            }
            var shoppingCartToDelete = await _shoppingCartRepository.GetShoppingCart(shoppingCartId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_shoppingCartRepository.DeleteShoppingCart(shoppingCartToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
