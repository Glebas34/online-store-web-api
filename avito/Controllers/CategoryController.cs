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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        [HttpGet("{catId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategory(int catId)
        {
            if (!_categoryRepository.CategoryExists(catId)) {
                return NotFound();
            }
            var category = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategory(catId));
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(category);
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetCategories(int catId)
        {
            if (!_categoryRepository.CategoryExists(catId))
            {
                return NotFound();
            }
            var categories = _mapper.Map<CategoryDto>(await _categoryRepository.GetCategories());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }

        [HttpPost("{catCreate}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory(CategoryDto catCreate) {
            if(catCreate == null) {
                return BadRequest(ModelState);
            }
            if(!_categoryRepository.CategoryExists(catCreate.Id))
            {
                ModelState.AddModelError("", "Категория уже существует");
                return StatusCode(422, ModelState);
            }
            var category = _mapper.Map<Category>(catCreate);
            if (!_categoryRepository.CreateCategory(category))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500,ModelState);
            }
            return Ok("Успешно создано");
        }

        [HttpPut("{сategoryId}")]
        [ProducesResponseType(400)]
        public IActionResult UpdateCategory(int сategoryId, [FromBody] CategoryDto updatedCategory)
        {
            if (updatedCategory == null)
            {
                return BadRequest(ModelState);
            }
            if (сategoryId != updatedCategory.Id)
            {
                return BadRequest(ModelState);
            }
            if (_categoryRepository.CategoryExists(сategoryId))
            {
                return NotFound();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var categoryMap = _mapper.Map<Category>(updatedCategory);
            if (!_categoryRepository.UpdateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Что-то пошло не так при сохранении");
                return StatusCode(500, ModelState);
            }
            return Ok("Успешно обновлено");
        }

        [HttpDelete("{categoryId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId))
            {
                return NotFound();
            }
            var categoryToDelete = await _categoryRepository.GetCategory(categoryId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!_categoryRepository.DeleteCategory(categoryToDelete))
            {
                ModelState.AddModelError("", "Что-то пошло не так при удалении");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }
    }
}
