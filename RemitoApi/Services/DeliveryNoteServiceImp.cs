using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RemitoApi.DTOs;
using RemitoApi.Entities;
using RemitoApi.Exceptions;
using RemitoApi.Helpers;
using RemitoApi.Interfaces;
using RemitoApi.Models;
using RemitoApi.Repositories;
using RemitoApi.Validation;

namespace RemitoApi.Services
{
    public class DeliveryNoteServiceImp : IDeliveryNoteServie
    {
        private readonly IDeliveryNoteRepository _deliveryNoteRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;

        public DeliveryNoteServiceImp(IDeliveryNoteRepository deliveryNoteRepository, IMapper mapper, UserManager<IdentityUser> userManager = null)
        {
            _deliveryNoteRepository = deliveryNoteRepository;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<DeliveryNote> CreateDeliveryNoteAsync(DeliveryNoteCreateDTO createDTO)
        {
            var user = await _userManager.FindByIdAsync(createDTO.UserId);
            if (user == null)
            {
                throw new NotFoundException("User not found!", 404);
            }

            var deliveryN = _deliveryNoteRepository.GetAll()
               .Where(x => x.UserId.Equals(user.Id) && x.IsOpen == true)
               .FirstOrDefault();

            if (deliveryN != null)
            {
                throw new CustomException("Delivery note is Open!", 400);
            }

            var deliveryMap = new DeliveryNote
            {
                UserId = createDTO.UserId,
                ItemsProducts = new List<Items>(),
                IsOpen = true,
                CreateDate = DateTime.Now,
                Total = 0
            };

            var deliveryNote = _deliveryNoteRepository.Create(deliveryMap);
            _deliveryNoteRepository.Save();

            if (deliveryNote == null)
            {
                throw new CustomException("Something went wrong while saving", 500);
            }
            return deliveryNote;
        }



        public async Task<DeliveryNoteCloseDTO> CloseDeliveryNoteAsync(string userId, int noteId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var deliveryN = _deliveryNoteRepository.GetAll()
                .Where(x => x.UserId.Equals(user.Id) && x.Id == noteId && x.IsOpen == true)
                .FirstOrDefault();

            if (deliveryN == null)
            {
                throw new CustomException("Note id is not valid or note is closed", 400);
            }

            deliveryN.IsOpen = false;
            _deliveryNoteRepository.Update(deliveryN);

            return _mapper.Map<DeliveryNoteCloseDTO>(deliveryN);
        }

        public IQueryable<DeliveryNote> GetQueryable()
        {
            return _deliveryNoteRepository.GetAll();
        }

        public async Task<ActionResult<List<DeliveryNoteCloseDTO>>> GetAll(PaginationDTO paginationDTO)
        {
            var deliveryNotes = await _deliveryNoteRepository.GetAll().ToPaginate(paginationDTO).ToListAsync();
            return _mapper.Map<List<DeliveryNoteCloseDTO>>(deliveryNotes);
        }

        public DeliveryNoteCloseDTO GetByUserId(string UserId)
        {
            var deliveryN = _deliveryNoteRepository.GetAll()
                 .Where(x => x.UserId.Equals(UserId) && x.IsOpen == true)
                 .FirstOrDefault();

            if (deliveryN == null)
            {
                throw new CustomException("Note doesn't exist or is closed!", 400);
            }

            var deliveryNMapped = _mapper.Map<DeliveryNoteCloseDTO>(deliveryN);

            return deliveryNMapped;
        }
    }
}
