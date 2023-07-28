using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Service
{
    public interface ISellerService
    {
        Task<ServiceResponse<SellerRequestDto>> AddSellerAsync(SellerRequestDto sellerRequestDto);

    }
}
