using EcommerceRESTApi.DTOs;
using System.ComponentModel.DataAnnotations;
using System.Web.WebPages;

namespace EcommerceRESTApi.Validation
{
    public static class ValidationsForImputs
    {
        public static bool ValidateString(string toValidate)
        {
            if (!toValidate.Equals("string") && !toValidate.IsEmpty() && !toValidate.Equals(""))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateNumber(double number)
        {
            return number > 0;
        }

        public static bool ValidateProduct(ProductCreateDTO createDTO)
        {
            if (ValidateString(createDTO.ProductName)
                && ValidateNumber(createDTO.ProductTypeId)
                && ValidateNumber(createDTO.ProductOriginId)
                && ValidateNumber(createDTO.CategoryId)
                && ValidateNumber(createDTO.Price)
                && ValidateNumber(createDTO.Quantity))
            {
                return true;
            }
            return false;
        }

        public static bool ValidateDeliveryNote(ItemAdditionDTO item, string userId)
        {
            if (ValidateNumber(item.ProductId) && ValidateNumber(item.DeliveryNoteId) && ValidateNumber(item.Quantity) && ValidateString(userId))
            {
                return true;
            }
            return false;
        }
    }
}
