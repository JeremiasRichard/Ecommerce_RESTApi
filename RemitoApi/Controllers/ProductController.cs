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

    public class ProductController : Controller
    {
        public readonly IProduct _productRepository;
        public readonly IProductOrigin _productOriginRepository;
        public readonly ICategory _categoryRepository;

        public readonly IMapper _mapper;

        public ProductController(IProduct productRepository, IMapper mapper, ICategory categoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProduct([FromBody]ProductCreateDTO productCreate, [FromQuery] int originId,[FromQuery] int categoryId, [FromQuery] int productTypeId)
        {

            if (productCreate == null)
            {
                ModelState.AddModelError("", "Valid Category Type field is required!");
                return StatusCode(422, ModelState);
            }

            var product = _productRepository.GetAll()
                .Where(x => x.ProductName.Trim().ToLower() == productCreate.Name.TrimEnd().ToLower() && x.ProductOrigin.Id == productCreate.ProductOriginId)
                .FirstOrDefault();

            if (product != null)
            {
                ModelState.AddModelError("", "Product already exist!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = _categoryRepository.GetById(categoryId);
            
            if (category == null)
            {
                ModelState.AddModelError("", "Category not found");
                return StatusCode(404, ModelState);
            }
            var productMap = _mapper.Map<Product>(productCreate);
            productMap.Category = category;
         
            if (!_productRepository.Create(productMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully created");
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductToShowDTO>))]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme,Policy ="IsAdmin")]
        public IActionResult GettAllProducts()
        {
            var products = _mapper.Map<List<ProductToShowDTO>>(_productRepository.GetAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(products);
        }
    }
}
