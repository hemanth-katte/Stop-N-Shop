﻿using Stop_nShop.Utilities.Enums;

namespace Stop_nShop.DTOs.RequestDTOs
{
    public class ProductRequestDto
    {
        public string productName { get; set; }

        public string productCategory { get; set; }

        public string productBrand { get; set; }

        public int productQuantity { get; set; }

        public int price { get; set; }

        public ProductSize productSize { get; set; }

        public int sellerId { get; set; }


    }
}
