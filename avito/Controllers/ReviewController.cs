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
    public class ReviewController : ControllerBase
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ReviewController(IReviewRepository reviewRepository, IMapper mapper,
            IAppUserRepository appUserRepository, IProductRepository productRepository)
        {
            _reviewRepository = reviewRepository;
            _mapper = mapper;
            _appUserRepository = appUserRepository;
            _productRepository = productRepository;
        }
        [HttpGet("{reviewId:int}")]
        [ProducesResponseType(200, Type = typeof(ReviewDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetReview(int reviewId)
        {
            if (_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var review = _mapper.Map<ReviewDto > (await _reviewRepository.GetReview(reviewId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(review);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ReviewDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetProducts()
        {
            var reviews = _mapper.Map<List<ReviewDto>>(await _reviewRepository.GetReviews());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reviews);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateReview([FromQuery] int productId, [FromQuery] int reviewerId, [FromBody] ReviewDto reviewCreate)
        {
            if (reviewCreate == null)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.ReviewExists(reviewCreate.Id))
            {
                ModelState.AddModelError("", "Объявление уже существует");
                return StatusCode(422, ModelState);
            }
            var reviewMap = _mapper.Map<Review>(reviewCreate);
            var product = await _productRepository.GetProduct(productId);
            var reviewer = await _appUserRepository.GetAppUserById(reviewerId);
            reviewMap.Reviewer = reviewer;
            reviewMap.Product = product;
            if (!_reviewRepository.CreateReview(reviewMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{reviewId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        [ProducesResponseType(204)]
        public IActionResult UpdateReview(int reviewId, [FromBody] ReviewDto updatedReview)
        {
            if (updatedReview == null)
            {
                return BadRequest(ModelState);
            }
            if (reviewId != updatedReview.Id)
            {
                return BadRequest(ModelState);
            }
            if (_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var reviewMap = _mapper.Map<Review>(updatedReview);
            if (!_reviewRepository.UpdateReview(reviewMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно обновлено");
        }

        [HttpDelete("{reviewId:int}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteReview(int reviewId)
        {
            if (!_reviewRepository.ReviewExists(reviewId))
            {
                return NotFound();
            }
            var reviewToDelete = await _reviewRepository.GetReview(reviewId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_reviewRepository.DeleteReview(reviewToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
