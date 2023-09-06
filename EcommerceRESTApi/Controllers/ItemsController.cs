using Microsoft.AspNetCore.Mvc;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Services;
using EcommerceRESTApi.Validation;

namespace EcommerceRESTApi.Controllers
{
    [ApiController]
    [Route("Api/[Controller]")]

    public class ItemsController : Controller
    {
        private readonly ItemServiceImp _itemService;

        public ItemsController(ItemServiceImp itemService)
        {
            _itemService = itemService;
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult AddItemToDeliveryNote(ItemAdditionDTO itemAdditionDTO, string userId)
        {
            if (!ValidationsForImputs.ValidateDeliveryNote(itemAdditionDTO, userId))
            {
                ModelState.AddModelError("", "Check your imputs");
                return StatusCode(400, ModelState);
            }

            return Ok(_itemService.AddItemToNote(itemAdditionDTO, userId));
        }

        [HttpDelete]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [Route("RemoveItemFromDeliveryNote")]
        public IActionResult RemoveItemFromDeliveryNote(int itemId)
        {
            if (!ValidationsForImputs.ValidateNumber(itemId))
            {
                ModelState.AddModelError("", "Check your imputs");
                return StatusCode(400, ModelState);
            }
            return Ok(_itemService.DeleteItemFromDeliveryNote(itemId));
        }
    }
}
