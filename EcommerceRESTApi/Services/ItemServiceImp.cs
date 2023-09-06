using AutoMapper;
using EcommerceRESTApi.DTOs;
using EcommerceRESTApi.Entities;
using EcommerceRESTApi.Exceptions;
using EcommerceRESTApi.Interfaces;
using EcommerceRESTApi.Models;
using EcommerceRESTApi.Repositories;

namespace EcommerceRESTApi.Services
{
    public class ItemServiceImp : IItemService
    {
        private readonly IItemsRepository _itemsRepository;
        private readonly IDeliveryNoteRepository _deliveryNoteRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public ItemServiceImp(IItemsRepository itemsRepository, IMapper mapper, IDeliveryNoteRepository deliveryNoteRepository, IProductRepository productRepository)
        {
            _itemsRepository = itemsRepository;
            _mapper = mapper;
            _deliveryNoteRepository = deliveryNoteRepository;
            _productRepository = productRepository;
        }

        public ItemToShowDTO AddItemToNote(ItemAdditionDTO itemDTO, string userId)
        {
            using (var transaction = _itemsRepository.BeginTransaction())
            {
                ValidateDeliveryNoteCreateDTO(itemDTO);
                var product = _productRepository.GetById(itemDTO.ProductId);
                var deliveryNote = _deliveryNoteRepository.GetById(itemDTO.DeliveryNoteId);
                if (itemDTO.Quantity > product.Quantity || !deliveryNote.UserId.Equals(userId))
                {
                    throw new CustomException("Insufficient stock", 400);
                }

                var newItemMap = _mapper.Map<Items>(itemDTO);
                newItemMap.SubTotal = newItemMap.Quantity * product.Price;
                deliveryNote.Total += newItemMap.SubTotal;
                product.Quantity -= itemDTO.Quantity;
                if (_productRepository.Update(product) == null)
                {
                    throw new CustomException("Error while updating product stock", 500);
                }

                var it = _itemsRepository.Create(newItemMap);
                if (it == null)
                {
                    throw new CustomException("Error while creating item", 500);
                }

                _itemsRepository.Save();
                deliveryNote.ItemsProducts.Add(it);
                if (_deliveryNoteRepository.Update(deliveryNote) == null)
                {
                    transaction.Rollback();
                    throw new CustomException("Error while updating delivery note", 500);
                }

                transaction.Commit();
                return _mapper.Map<ItemToShowDTO>(newItemMap);
            }
        }

        public ItemToShowDTO DeleteItemFromDeliveryNote(int itemId)
        {
            using (var transaction = _itemsRepository.BeginTransaction())
            {
                var itemToDelete = _itemsRepository.GetById(itemId);

                if (itemToDelete == null)
                {
                    throw new CustomException("Item doesn't exist!", 400);
                }

                var deliveryNote = _deliveryNoteRepository.GetById(itemToDelete.DeliveryNoteId);

                var product = _productRepository.GetById(itemToDelete.ProductId);

                deliveryNote.Total -= itemToDelete.SubTotal;
                product.Quantity += itemToDelete.Quantity;

                if (_productRepository.Update(product) == null)
                {
                    throw new CustomException("Error while updating product stock", 500);
                }

                deliveryNote.ItemsProducts.Remove(itemToDelete);
                _itemsRepository.Remove(itemToDelete);
                _itemsRepository.Save();

                if (_deliveryNoteRepository.Update(deliveryNote) == null)
                {
                    transaction.Rollback();
                    throw new CustomException("Error while updating delivery note", 500);
                }
                transaction.Commit();
                return _mapper.Map<ItemToShowDTO>(itemToDelete);
            }
        }

        public void ValidateDeliveryNoteCreateDTO(ItemAdditionDTO itemAdditionDTO)
        {
            if (!_productRepository.Exist(itemAdditionDTO.ProductId))
            {
                throw new NotFoundException("Product doesn't exist!", 404);
            }

            var dn = _deliveryNoteRepository.GetById(itemAdditionDTO.DeliveryNoteId);
            if (dn == null)
            {
                throw new NotFoundException("Note doesn't exist!", 404);
            }

            if (!dn.IsOpen)
            {
                throw new CustomException("Note is closed!", 400);
            }
        }
    }
}