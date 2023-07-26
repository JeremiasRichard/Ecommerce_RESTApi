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

    public class CategoryTypeController : Controller
    {
        private readonly ICategoryType _categoryTypeRepository;
        private readonly IMapper _mapper;

        public CategoryTypeController(ICategoryType categoryType, IMapper mapper)
        {
            _categoryTypeRepository = categoryType;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductOrigin([FromBody] CategoryTypeCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                ModelState.AddModelError("", "Name field is required!");
                return StatusCode(422, ModelState);
            }

            var productType = _categoryTypeRepository.GetAll()
                .Where(x => x.Name.Trim().ToUpper() == createDTO.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (productType != null)
            {
                ModelState.AddModelError("", "Category Type already exist!");
                return StatusCode(422, ModelState);

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryTypeMap = _mapper.Map<CategoryType>(createDTO);

            if (!_categoryTypeRepository.Create(categoryTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created!");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryType>))]
        public IActionResult GetAllProductTypes()
        {
            var categoryTypes = _mapper.Map<List<CategoryType>>(_categoryTypeRepository.GetAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(categoryTypes);
        }
    }
}
