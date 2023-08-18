namespace Stop_nShop.DTOs.RequestDTOs
{
    public class FilterViewProductRequestDto
    {
        public string? name { get; set; }

        public string? category { get; set; }

        public string? brand { get; set; }

        public int? sellerId { get; set; }

    }
}
