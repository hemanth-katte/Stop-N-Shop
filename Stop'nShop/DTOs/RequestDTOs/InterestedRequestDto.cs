using Stop_nShop.Models.Enums;

namespace Stop_nShop.DTOs.RequestDTOs
{
    public class InterestedRequestDto
    {
        public int userId { get; set; }

        public int sellerId { get; set; }

        public int productId { get; set; }

        public InterestedStatus status { get; set; }

    }
}
