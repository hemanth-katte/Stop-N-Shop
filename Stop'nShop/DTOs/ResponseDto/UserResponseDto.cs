using Stop_nShop.Models.Enums;

namespace Stop_nShop.DTOs.ResponseDto
{
    public class UserResponseDto
    {
        public int userId { get; set; }

        public string firstName { get; set; }

        public string lastName { get; set; }

        public string userEmail { get; set; }

        public string userPhone { get; set; }

        public string userAddress { get; set; }

        public UserStatus userStatus { get; set; }


    }
}
