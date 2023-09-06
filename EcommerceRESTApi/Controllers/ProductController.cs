using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EcommerceRESTApi.DataBase;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Entities;
using EcommerceRESTApi.Helpers;
using EcommerceRESTApi.Interfaces;
using EcommerceRESTApi.Services;
using EcommerceRESTApi.Validation;
using System.Linq;

namespace EcommerceRESTApi.Controllers
{
    [ApiController]
    [Route("Api/Product")]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProductController : Controller
    {
        private readonly ProductServiceImp _productService;

        public ProductController(ProductServiceImp productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody] ProductCreateDTO productCreate)
        {
            if (!ModelState.IsValid || !ValidationsForImputs.ValidateProduct(productCreate))
            {
                return BadRequest("Please check your imptus");
            }

            return Ok(_productService.CreateProduct(productCreate));
        }


        [HttpGet]
        [Route("/Pagination")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductToShowDTO>))]
        [ResponseCache(Duration = 10)] // cache por segundo
        [AllowAnonymous]
        public async Task<ActionResult<List<ProductToShowDTO>>> GettAllProducts([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _productService.GetQueryable();
            await HttpContext.InsertPaginationParametersInHeader(queryable, paginationDTO.Page, paginationDTO.RecordsPerPage);

            return await _productService.GetAll(paginationDTO);
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult DeleteProduct(int productId)
        {
            if (!ValidationsForImputs.ValidateNumber(productId))
            {
                return BadRequest("Please insert a valid product Id");
            }

            return Ok(_productService.DeleteProduct(productId));
        }

        [HttpPut]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        //[Authorize(Policy = "AdminPolicy")]
        public ActionResult<ProductCreateDTO> UpdateProduct([FromBody] ProductCreateDTO product)
        {
            if (!ValidationsForImputs.ValidateNumber(product.ProductTypeId) || !ValidationsForImputs.ValidateNumber(product.ProductOriginId))
            {
                return BadRequest("Please insert a valid product Id");
            }

            return Ok(_productService.Update(product));
        }
    }
}
