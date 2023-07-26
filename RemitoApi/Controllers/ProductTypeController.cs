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

    public class ProductTypeController : Controller
    {
        private readonly IProductType _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductTypeController(IProductType productType, IMapper mapper)
        {
            _productTypeRepository = productType;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductType([FromBody] ProductTypeCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                ModelState.AddModelError("", "Name field is required!");
                return StatusCode(422, ModelState);
            }

            var productType = _productTypeRepository.GetAll()
                .Where(x => x.Name.Trim().ToUpper() == createDTO.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (productType != null)
            {
                ModelState.AddModelError("", "Product Type already exist!");
                return StatusCode(422, ModelState);

            }
            
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productTypeMap = _mapper.Map<ProductType>(createDTO);

            if(!_productTypeRepository.Create(productTypeMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created!");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductType>))]
        public IActionResult GetAllProductTypes()
        {
            var productTypes = _mapper.Map<List<ProductTypeCreateDTO>>(_productTypeRepository.GetAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(productTypes);
        }

        [HttpGet("{Id}")]
        [ProducesResponseType(200, Type = typeof(ProductTypeCreateDTO))]
        [ProducesResponseType(400)]
        public IActionResult GetProductTypeById(int Id)
        {
            if (!_productTypeRepository.Exist(Id))
            {
                return NotFound();
            }
            
            var productType = _mapper.Map<ProductTypeCreateDTO>(_productTypeRepository.GetById(Id));

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(productType);
        }
    }
}
