using Stop_nShop.Models.Enums;

namespace Stop_nShop.DTOs.ResponseDto
{
    public class PlaceOrderResponse
    {
      //  public int productId { get; set; }

        public int userId { get; set; }

        public int orderId { get; set; }

        public int sellerId { get; set; }

        public int quantity { get; set; }

        public OrderStatus status { get; set; }

    }
}
