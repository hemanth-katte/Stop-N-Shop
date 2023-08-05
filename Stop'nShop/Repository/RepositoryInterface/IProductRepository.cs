using Stop_nShop.Models;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository.RepositoryInterface
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
