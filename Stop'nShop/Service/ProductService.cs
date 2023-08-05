using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Service
{
    public class ProductService : IProductService
    {
        public readonly IProductRepository _productRepository;
       // public readonly ISellerRepository sellerRepository;

        public ProductService(IProductRepository productRepository) 
        { 
            _productRepository = productRepository;
           // this.sellerRepository = sellerRepository;
        }


        public async Task<ServiceResponse<ProductResponseDto>> AddProductAsync(ProductRequestDto productRequestDto)
        {
           // Seller seller = await sellerRepository.find

            Product product = new Product()
            {
                productName = productRequestDto.productName,
                category = productRequestDto.productCategory,
                brand = productRequestDto.productBrand,
                quantity = productRequestDto.productQuantity,
                sellerId = productRequestDto.sellerId,
                size = productRequestDto.productSize
            };

            var result = await _productRepository.AddProductAsync(product);

            var response = new ServiceResponse<ProductResponseDto>()
            {
                Data = result.Success ? new ProductResponseDto()
                {
                    productName = result.Data.productName,
                    productCategory = result.Data.category,
                    productBrand = result.Data.brand,
                    productQuantity = result.Data.quantity,
                    productId = result.Data.productId,
                    sellerId = result.Data.sellerId

                } : null,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage,
                ResultMessage = result.ResultMessage
                
            };

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int productId)
        {
            

            var productResponse = await _productRepository.DeleteProductAsync(productId);

            return productResponse;

            
        }

        public async Task<ServiceResponse<List<Product>>> ViewAllProductsSellerAsync(int sellerId)
        {
            var response = await _productRepository.ViewAllProductsSellerAsync(sellerId);

            return response;
        }

        public async Task<ServiceResponse<List<FilterViewProductsResponseDto>>> FilterViewProductsUser(string productName, string category, string brand, int sellerId)
        {
            var result = new ServiceResponse<List<Product>>();

            if(string.IsNullOrEmpty(productName) && string.IsNullOrEmpty(category) && string.IsNullOrEmpty(brand) && sellerId == 0)
            {
                result = await _productRepository.FindAllProductsUser();
            }

            else result = await _productRepository.FilterViewProductsUser(productName,category, brand, sellerId);

            List<FilterViewProductsResponseDto> products = new List<FilterViewProductsResponseDto>();

            if (result.Success)
            {
                result.Data.ForEach(p =>
                {
                    FilterViewProductsResponseDto product = new FilterViewProductsResponseDto()
                    {
                        productId = p.productId,
                        productName = p.productName,
                        productBrand = p.brand,
                        productCategory = p.category,
                        productQuantity = p.quantity,
                        sellerId = p.sellerId
                    };
                    products.Add(product);
                });
            }

            return new ServiceResponse<List<FilterViewProductsResponseDto>>()
            {
                Data = products,
                Success = result.Success,
                ResultMessage = products.Count == 0 ? "No products found" : "Here are your products",
                ErrorMessage = result.ErrorMessage
                
            };

            
        }

    }
}
