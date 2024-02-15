using AutoMapper;
using avito.Dto;
using avito.Interfaces;
using avito.Models;
using Microsoft.AspNetCore.Mvc;

namespace avito.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
        private readonly IMapper _mapper;
        public ProductController(IProductRepository productRepository, ICategoryRepository categoryRepository, IReviewRepository reviewRepository, IShoppingCartItemRepository shoppingCartItemRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _reviewRepository = reviewRepository;
            _shoppingCartItemRepository = shoppingCartItemRepository;
            _mapper = mapper;
        }

        [HttpGet("{productId:int}")]
        [ProducesResponseType(200, Type = typeof(ProductDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProduct(int productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }
            var product = _mapper.Map<ProductDto>(await _productRepository.GetProduct(productId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(product);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProducts() {
            var products = _mapper.Map<List<ProductDto>>(await _productRepository.GetProducts());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(products);
        }

        [HttpGet("{productId:int}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetProductRating(int productId)
        {
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }
            var rating = _productRepository.GetProductRating(productId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateProduct([FromQuery] int catId, [FromBody] ProductDto productCreate) {
            if(productCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (_productRepository.ProductExists(productCreate.Id))
            {
                ModelState.AddModelError("", "Объявление уже существует");
                return StatusCode(422, ModelState);
            }
            var productMap = _mapper.Map<Product>(productCreate);
            var category = await _categoryRepository.GetCategory(catId);
            productMap.Category = category;
            if (!_productRepository.CreateProduct(productMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{productId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateProduct(int productId,[FromBody]ProductDto updatedProduct)
        {
            if (updatedProduct==null)
            {
                return BadRequest(ModelState);
            }
            if(productId!=updatedProduct.Id) {
                return BadRequest(ModelState);
            }
            if(!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }
            if(!ModelState.IsValid) {
                return BadRequest();
            }
            var productMap = _mapper.Map<Product>(updatedProduct);
            if (!_productRepository.UpdateProduct(productMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно обновлено");
        }

        [HttpDelete("{productId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteProduct(int productId) {
            if (!_productRepository.ProductExists(productId))
            {
                return NotFound();
            }
            var productToDelete = await _productRepository.GetProduct(productId);
            var reviewsToDelete = await _reviewRepository.GetReviewsOfProduct(productId);
            var itemsToDelete = await _shoppingCartItemRepository.GetShoppingCartItemsOfProduct(productId); 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_shoppingCartItemRepository.DeleteShoppingCartItems(itemsToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении товаров из корзины");
                return StatusCode(500, ModelState);
            }
            if (!_reviewRepository.DeleteReviews(reviewsToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении отзывов");
                return StatusCode(500, ModelState);
            }
            if (!_productRepository.DeleteProduct(productToDelete)) {
                ModelState.AddModelError("", "Что-то пошло не так при удалении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
