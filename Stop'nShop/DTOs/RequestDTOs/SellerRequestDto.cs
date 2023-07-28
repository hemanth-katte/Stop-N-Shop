using System.ComponentModel.DataAnnotations;

namespace Stop_nShop.DTOs.RequestDTOs
{
    public class SellerRequestDto
    {
        public string sellerName { get; set; }

        [Required]
        public string sellerEmail { get; set; }

        public string sellerPassword { get; set; }

        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Invalid phone number")]
        public string sellerPhone { get; set; }

        public string sellerAddress { get; set; }


    }
}
