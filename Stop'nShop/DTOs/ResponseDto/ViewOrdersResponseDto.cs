using Stop_nShop.Models;

namespace Stop_nShop.DTOs.ResponseDto
{
    public class ViewOrdersResponseDto
    {
        public int userId { get; set; }

        public ICollection<Orders> orders { get; set; }

    }
}
