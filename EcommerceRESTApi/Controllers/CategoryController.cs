using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Exceptions;
using EcommerceRESTApi.Helpers;
using EcommerceRESTApi.Services;
using EcommerceRESTApi.Validation;

namespace EcommerceRESTApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class CategoryController : Controller
    {
        private readonly CategoryServiceImp _categoryService;
        public CategoryController(CategoryServiceImp categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory(CategoryCreateDTO createDTO)
        {
            if (!ModelState.IsValid || !ValidationsForImputs.ValidateString(createDTO.Name))
            {
                ModelState.AddModelError("", "Valid Category Type field is required!");
                return StatusCode(400, ModelState);
            }

            return Ok(_categoryService.CreateCategory(createDTO));
        }


        [HttpGet]
        [Route("/CategoryPagination")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryToShowDTO>))]
        [ResponseCache(Duration = 10)] // cache por segundo
        [AllowAnonymous]
        public async Task<ActionResult<List<CategoryToShowDTO>>> GettAllProducts([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _categoryService.GetQueryable();
            await HttpContext.InsertPaginationParametersInHeader(queryable, paginationDTO.Page, paginationDTO.RecordsPerPage);

            return await _categoryService.GetAll(paginationDTO);
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetAllProductByCategory(int categoryId)
        {
            var categories = _categoryService.GetAllProductsByCategory(categoryId);

            if (categories == null)
            {
                BadRequest();
            }

            return Ok(categories);
        }
    }
}
