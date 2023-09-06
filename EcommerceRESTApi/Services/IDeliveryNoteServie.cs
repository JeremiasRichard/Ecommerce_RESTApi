using Microsoft.AspNetCore.Mvc;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Models;

namespace EcommerceRESTApi.Services
{
    public interface IDeliveryNoteServie
    {
        public Task<DeliveryNote> CreateDeliveryNoteAsync(DeliveryNoteCreateDTO createDTO);
        public Task<DeliveryNoteCloseDTO> CloseDeliveryNoteAsync(string userId, int noteId);
        public IQueryable<DeliveryNote> GetQueryable();
        public Task<ActionResult<List<DeliveryNoteCloseDTO>>> GetAll(PaginationDTO paginationDTO);
        public DeliveryNoteCloseDTO GetByUserId(string UserId);
    }
}
