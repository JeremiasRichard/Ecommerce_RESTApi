using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemitoApi.DTOs;
using RemitoApi.Entities;
using RemitoApi.Interfaces;

namespace RemitoApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CategoryController : Controller
    {
        private readonly ICategory _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategory category, IMapper mapper, ICategoryType categoryType)
        {
            _categoryRepository = category;
            _mapper = mapper; 
        }

        [HttpPost("{categoryTypeId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryCreateDTO createDTO,int categoryTypeId)
        {
            if (createDTO == null || createDTO.CategoryTypeId != categoryTypeId)
            {
                ModelState.AddModelError("", "Valid Category Type field is required!");
                return StatusCode(422, ModelState);
            }

            var category = _categoryRepository.GetAll()
                .Where(x => x.CategoryTypeId == createDTO.CategoryTypeId)
                .FirstOrDefault();

            if (category != null)
            {
                ModelState.AddModelError("", "Category already exist!");
                return StatusCode(422, ModelState);

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(createDTO);

            if (!_categoryRepository.Create(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created!");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryToShowDTO>))]
        public IActionResult GetAllCategories()
        {
            var categories = _mapper.Map<List<CategoryToShowDTO>>(_categoryRepository.GetAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categories);
        }
    }
}
