using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Service.ServiceInterface
{
    public interface IProductService
    {
        Task<ServiceResponse<ProductResponseDto>> AddProductAsync(ProductRequestDto productRequestDto);

        Task<ServiceResponse<bool>> DeleteProductAsync(int productId);

        Task<ServiceResponse<List<Product>>> ViewAllProductsSellerAsync(int sellerId);

        Task<ServiceResponse<List<FilterViewProductsResponseDto>>> FilterViewProductsUser(string productName, string category, string brand, int sellerId);
    }
}
