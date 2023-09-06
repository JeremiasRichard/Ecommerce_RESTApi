using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemitoApi.DTOs;
using RemitoApi.Entities;
using RemitoApi.Helpers;
using RemitoApi.Interfaces;
using RemitoApi.Repositories;
using RemitoApi.Services;
using RemitoApi.Validation;

namespace RemitoApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class ProductTypeController : Controller
    {
        private readonly ProductTypeServiceImp _productTypeService;

        public ProductTypeController(ProductTypeServiceImp productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductType([FromBody] ProductTypeCreateDTO createDTO)
        {
            if (!ValidationsForImputs.ValidateString(createDTO.Name))
            {
                ModelState.AddModelError(" ", "Name field is required!");
                return StatusCode(422, ModelState);
            }

            return Ok(_productTypeService.CreateProductType(createDTO));
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ProductTypeToShowDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsByTypeById(int Id)
        {
            if (!ValidationsForImputs.ValidateNumber(Id))
            {
                ModelState.AddModelError(" ", "Please enter a valid Id!");
                return StatusCode(400, ModelState);
            }

            var productType = _productTypeService.GetAllProductsByType(Id);

            if (!productType.Any())
            {
                //return NotFound();
            }

            return Ok(productType);
        }

        [HttpGet]
        [Route("/ProductTypePagination")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductTypeToShowDTO>))]
        [ResponseCache(Duration = 10)] // cache por segundo
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductTypeToShowDTO>>> GettAllProducts([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _productTypeService.GetQueryable();
            await HttpContext.InsertPaginationParametersInHeader(queryable, paginationDTO.Page, paginationDTO.RecordsPerPage);

            return await _productTypeService.GetAll(paginationDTO);
        }
    }
}
