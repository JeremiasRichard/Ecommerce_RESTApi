using RemitoApi.DTOs;

namespace RemitoApi.Services
{
    public interface IItemService
    {
        public ItemToShowDTO AddItemToNote(ItemAdditionDTO itemDTO, string userId);
        public ItemToShowDTO DeleteItemFromDeliveryNote(int itemId);
        public void ValidateDeliveryNoteCreateDTO(ItemAdditionDTO itemAdditionDTO);
    }
}
