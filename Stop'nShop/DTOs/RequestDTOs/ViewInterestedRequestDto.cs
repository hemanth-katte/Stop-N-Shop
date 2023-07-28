using Stop_nShop.Models.Enums;

namespace Stop_nShop.DTOs.RequestDTOs
{
    public class ViewInterestedRequestDto
    {
        public int userId { get; set; }

        public InterestedStatus status { get; set; }

    }
}
