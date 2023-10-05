using Stop_nShop.Models;

namespace Stop_nShop.DTOs.ResponseDto
{
    public class ViewInterestedResponseDto
    {
        public int userId { get; set; }

        public List<Product> interestedList { get; set; }

    }
}
