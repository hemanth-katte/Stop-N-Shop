using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository
{
    public interface ISellerRepository
    {
        Task<ServiceResponse<Seller>> AddSellerAsync(Seller seller);

    }
}
