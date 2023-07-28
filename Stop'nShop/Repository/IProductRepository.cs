using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository
{
    public interface IProductRepository
    {

        Task<ServiceResponse<Product>> AddProductAsync(Product product);

        Task<ServiceResponse<bool>> DeleteProductAsync(int productId);

        Task<ServiceResponse<Product>> FindProductById(int productId);

        Task<ServiceResponse<List<Product>>> ViewAllProductsSellerAsync(int sellerId);

        Task<ServiceResponse<List<Product>>> FilterViewProductsUser(string productName, string category, string brand, int sellerIdroductsRequestDto);

        Task<ServiceResponse<List<Product>>> FindAllProductsUser();
    }
}
