using Stop_nShop.Models;

namespace Stop_nShop.DTOs.ResponseDto
{
    public class ViewInterestedResponseDto
    {
        public int userId { get; set; }

        public ICollection<Product> interestedList { get; set; }

    }
}
