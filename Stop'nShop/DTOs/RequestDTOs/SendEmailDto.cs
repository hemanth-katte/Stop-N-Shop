﻿namespace Stop_nShop.DTOs.RequestDTOs
{
    public class SendEmailDto
    {
        public string  To { get; set; } = string.Empty;

        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;
    }
}
