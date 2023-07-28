namespace Stop_nShop.DTOs.RequestDTOs
{
    public class PlaceOrderDto
    {
        public int productId { get; set; }

        public int sellerId { get; set; }

        public int userId { get; set; }

        public int quantity { get; set; }

        public int price { get; set; }
    }
}
