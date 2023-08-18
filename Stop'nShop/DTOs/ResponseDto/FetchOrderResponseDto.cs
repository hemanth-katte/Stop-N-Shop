using Stop_nShop.Models.Enums;


namespace Stop_nShop.DTOs.ResponseDto
{
    public class FetchOrderResponseDto
    {
        public int orderId { get; set; }

        public DateTime orderDateTime { get; set; }

        public OrderStatus orderStatus { get; set; }

        public int userId { get; set;}

        public int totalPrice { get; set;}

    }
}
