using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository.RepositoryInterface
{
    public interface ISellerRepository
    {
        Task<ServiceResponse<Seller>> AddSellerAsync(Seller seller);

        Task<ServiceResponse<Seller>> AuthenticateSeller(string sellerEmail, string sellerPassword);

    }
}
