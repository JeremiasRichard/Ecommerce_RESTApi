using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RemitoApi.DTOs;
using RemitoApi.DTOs.Secutiry;
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

    public class ProductOriginController : Controller
    {
        private readonly ProductOriginServiceImp _productOriginService;

        public ProductOriginController(ProductOriginServiceImp productOriginService)
        {
            _productOriginService = productOriginService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductOrigin([FromBody] ProductOriginCreateDTO createDTO)
        {
            if (!ModelState.IsValid || !ValidationsForImputs.ValidateString(createDTO.Name))
            {
                ModelState.AddModelError("", "Name field is required!");
                return StatusCode(400, ModelState);
            }

            var product = _productOriginService.CreateProductOrigin(createDTO);

            if (product != null)
            {
                return Ok(createDTO);
            }

            return BadRequest();
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ProductOriginToShowDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetProductsByOriginId(int Id)
        {
            if (!ValidationsForImputs.ValidateNumber(Id))
            {
                ModelState.AddModelError(" ", "Please enter a valid Id!");
                return StatusCode(400, ModelState);
            }

            var productType = _productOriginService.GetAllProductsByOrigin(Id);

            if (!productType.Any())
            {
                return NotFound();
            }

            return Ok(productType);
        }

        [HttpGet]
        [Route("/ProductOriginPagination")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductOrigin>))]
        [ResponseCache(Duration = 10)] // cache por segundo
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductOriginToShowDTO>>> GettAllProducts([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _productOriginService.GetAllProductOrigin();
            await HttpContext.InsertPaginationParametersInHeader(queryable, paginationDTO.Page, paginationDTO.RecordsPerPage);

            return await _productOriginService.GetAll(paginationDTO);
        }
    }
}
