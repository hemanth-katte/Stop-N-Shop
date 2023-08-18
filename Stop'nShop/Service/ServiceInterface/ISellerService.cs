using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Service.ServiceInterface
{
    public interface ISellerService
    {
        Task<ServiceResponse<SellerRequestDto>> AddSellerAsync(SellerRequestDto sellerRequestDto);

        Task<ServiceResponse<string>> GenerateToken(string sellerEmail, string sellerPassword);

    }
}
