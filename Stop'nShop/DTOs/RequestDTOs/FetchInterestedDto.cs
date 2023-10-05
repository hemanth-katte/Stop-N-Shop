using Stop_nShop.Models.Enums;

namespace Stop_nShop.DTOs.RequestDTOs
{
    public class FetchInterestedDto
    {
        public int userId {  get; set; }

        public InterestedStatus interestedStatus { get; set;}

    }
}
