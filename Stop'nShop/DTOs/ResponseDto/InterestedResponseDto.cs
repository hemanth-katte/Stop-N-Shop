using Stop_nShop.Utilities.Enums;

namespace Stop_nShop.DTOs.ResponseDto
{
    public class InterestedResponseDto
    {
        public int interestedId { get; set; }

        public int userId { get; set; }

        public int sellerId { get; set; }

        public int productId { get; set; }

        public InterestedStatus status { get; set; }

    }
}
