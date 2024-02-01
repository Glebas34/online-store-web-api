using AutoMapper;
using avito.Dto;
using avito.Interfaces;
using avito.Models;
using avito.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartItemController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        private readonly IMapper _mapper;
        public ShoppingCartItemController(IShoppingCartItemRepository shoppingCartItemRepository, IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository, IMapper mapper)
        {
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _productRepository = productRepository;
            _shoppingCartRepository = shoppingCartRepository;
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(ShoppingCartItemDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShoppingCartItem(int id)
        {
            if (_shoppingCartItemRepository.ShoppingCartItemExists(id))
            {
                return NotFound();
            }
            var shoppingCartItem = _mapper.Map<ShoppingCartItemDto>(await _shoppingCartItemRepository.GetShoppingCartItem(id));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(shoppingCartItem);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetShoppingCartItems()
        {
            var shoppingCartItems = _mapper.Map<List<ProductDto>>(await _shoppingCartItemRepository.GetShoppingCartItems());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(shoppingCartItems);
        }

        [HttpPost("{productCreate}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateShoppingCartItem([FromQuery] int shoppingCartId, [FromQuery] int productId, [FromBody] ShoppingCartItemDto shoppingCartItemCreate)
        {
            if (shoppingCartItemCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!_shoppingCartItemRepository.ShoppingCartItemExists(shoppingCartItemCreate.Id))
            {
                ModelState.AddModelError("", "Объявление уже существует");
                return StatusCode(422, ModelState);
            }
            var shoppingCartItemMap = _mapper.Map<ShoppingCartItem>(shoppingCartItemCreate);
            var shoppingCart = await _shoppingCartRepository.GetShoppingCart(shoppingCartId);
            var product = await _productRepository.GetProduct(productId);
            shoppingCartItemMap.Product = product;
            shoppingCartItemMap.ShoppingCart = shoppingCart;
            if (!_shoppingCartItemRepository.CreateShoppingCartItem(shoppingCartItemMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{shoppingCartItemId}")]
        [ProducesResponseType(400)]
        public IActionResult UpdateShoppingCartItem(int shoppingCartItemId, [FromBody] ShoppingCartItemDto updatedShoppingCartItem)
        {
            if (updatedShoppingCartItem == null)
            {
                return BadRequest(ModelState);
            }
            if (shoppingCartItemId != updatedShoppingCartItem.Id)
            {
                return BadRequest(ModelState);
            }
            if (_shoppingCartItemRepository.ShoppingCartItemExists(shoppingCartItemId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var shoppingCartItemMap = _mapper.Map<ShoppingCartItem>(updatedShoppingCartItem);
            if (!_shoppingCartItemRepository.UpdateShoppingCartItem(shoppingCartItemMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно обновлено");
        }

        [HttpDelete("{shoppingCartItemId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteShoppingCartItem(int shoppingCartItemId)
        {
            if (!_shoppingCartItemRepository.ShoppingCartItemExists(shoppingCartItemId))
            {
                return NotFound();
            }
            var shoppingCartItemToDelete = await _shoppingCartItemRepository.GetShoppingCartItem(shoppingCartItemId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_shoppingCartItemRepository.DeleteShoppingCartItem(shoppingCartItemToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
