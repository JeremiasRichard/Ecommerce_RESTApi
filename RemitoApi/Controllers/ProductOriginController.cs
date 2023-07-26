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

    public class ProductOriginController : Controller
    {
        private readonly IProductOrigin _productOriginRepository;
        private readonly IMapper _mapper;

        public ProductOriginController(IProductOrigin productOrigin, IMapper mapper)
        {
            _productOriginRepository = productOrigin;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult CreateProductOrigin([FromBody] ProductOriginCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                ModelState.AddModelError("", "Name field is required!");
                return StatusCode(422, ModelState);
            }

            var productType = _productOriginRepository.GetAll()
                .Where(x => x.Name.Trim().ToUpper() == createDTO.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (productType != null)
            {
                ModelState.AddModelError("", "Product Origin already exist!");
                return StatusCode(422, ModelState);

            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var productOriginMap = _mapper.Map<ProductOrigin>(createDTO);

            if (!_productOriginRepository.Create(productOriginMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created!");
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductOrigin>))]
        public IActionResult GetAllProductTypes()
        {
            var productOrigins = _mapper.Map<List<ProductOrigin>>(_productOriginRepository.GetAll());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(productOrigins);
        }
    }
}
