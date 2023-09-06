using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Helpers;
using EcommerceRESTApi.Models;
using EcommerceRESTApi.Services;
using EcommerceRESTApi.Validation;

namespace EcommerceRESTApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]
    public class DeliveryNoteController : Controller
    {
        private readonly DeliveryNoteServiceImp _deliveryNoteService;

        public DeliveryNoteController(DeliveryNoteServiceImp deliveryNoteService)
        {
            _deliveryNoteService = deliveryNoteService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDeliveryNote(DeliveryNoteCreateDTO createDTO)
        {
            if (!ValidationsForImputs.ValidateString(createDTO.UserId))
            {
                ModelState.AddModelError("", "Invalid user ID");
                return StatusCode(400, ModelState);
            }

            return Ok(await _deliveryNoteService.CreateDeliveryNoteAsync(createDTO));
        }

        [HttpPatch]
        public async Task<IActionResult> CloseDeliveryNote(string userId, int noteId)
        {
            if (!ValidationsForImputs.ValidateString(userId) || !ValidationsForImputs.ValidateNumber(noteId))
            {
                ModelState.AddModelError("", "Invalid user ID or noteId");
                return StatusCode(400, ModelState);
            }

            var dn = await _deliveryNoteService.CloseDeliveryNoteAsync(userId, noteId);
            return Ok(dn);
        }

        [HttpGet]
        [Route("/DeliveryNotesPagination")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DeliveryNoteCloseDTO>))]
        [ResponseCache(Duration = 10)] // cache por segundo
        [AllowAnonymous]
        public async Task<ActionResult<List<DeliveryNoteCloseDTO>>> GettAllNotes([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = _deliveryNoteService.GetQueryable();
            await HttpContext.InsertPaginationParametersInHeader(queryable, paginationDTO.Page, paginationDTO.RecordsPerPage);

            return await _deliveryNoteService.GetAll(paginationDTO);
        }


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(DeliveryNoteCloseDTO))]
        public IActionResult GetMyCurrentNote(string userId)
        {
            if (!ValidationsForImputs.ValidateString(userId))
            {
                ModelState.AddModelError("", "Invalid user ID!");
                return StatusCode(400, ModelState);
            } 

            return  Ok( _deliveryNoteService.GetByUserId(userId));
        }
    }
}
