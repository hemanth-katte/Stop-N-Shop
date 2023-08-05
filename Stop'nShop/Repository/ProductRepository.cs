using Microsoft.EntityFrameworkCore;
using Stop_nShop.Data;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;


namespace Stop_nShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        public readonly StopAndShopDBContext stopAndShopDBContext;

        public ProductRepository(StopAndShopDBContext stopAndShopDBContext)
        {
            this.stopAndShopDBContext = stopAndShopDBContext;
        }

        public async Task<ServiceResponse<Product>> AddProductAsync(Product product)
        {
            try
            {

                bool validateSeller = ValidateSeller(product.sellerId);

                if(!validateSeller)
                {
                    return new ServiceResponse<Product>()
                    {
                        Success = false,
                        Data = null,
                        ErrorMessage = "Seller doesn't exist",
                        ResultMessage = "Please register seller"
                    };
                }
                

                bool validate = ValidateProductExixts(product);

                if(validate)
                {
                    return new ServiceResponse<Product>()
                    {
                        Data = null,
                        Success = false,
                        ErrorMessage = "Product already exists",
                        ResultMessage = "Use 'add-more' products in view products"
                    };
                }

                await stopAndShopDBContext.Products.AddAsync(product);
                await stopAndShopDBContext.SaveChangesAsync();

                return new ServiceResponse<Product>
                {
                    Success = true,
                    Data = product,
                    ResultMessage = "Product added successfully!"
                };
            } catch (Exception ex)
            {
                return new ServiceResponse<Product> {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "There is some error, please try again later"
                };
            }
        }

        public async Task<ServiceResponse<bool>> DeleteProductAsync(int productId)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var product = await stopAndShopDBContext.Products.FindAsync(productId);

                if (product == null)
                {   
                    response.Success = false;
                    response.ErrorMessage = "Product not found.";
                    return response;
                }

                stopAndShopDBContext.Products.Remove(product);
                await stopAndShopDBContext.SaveChangesAsync();

                response.Data = true;
                response.Success = true;
                response.ResultMessage = "Product successfully deleted";

                    
            }
            catch (Exception ex)
            {   
                response.Data = false;
                response.Success = false;
                response.ErrorMessage = "An error occurred while deleting the product.";
                response.ResultMessage = "Please try again later!";

            }

            return response;
        }

        public async Task<ServiceResponse<Product>> FindProductById(int productId)
        {
            

            try
            {
                Product product = await stopAndShopDBContext.Products.FindAsync(productId);

                if (product == null)
                {
                    return new ServiceResponse<Product>
                    {
                        Data = null,
                        Success = false,
                        ErrorMessage = "Product missing from Database"
                    };
                }


                return new ServiceResponse<Product>
                {
                    Success = true,
                    Data = product,
                    ResultMessage = "OK"
                };


            }
            catch(Exception ex) 
            {
                return new ServiceResponse<Product>
                {   
                    Data = null,
                    Success = false,
                    ErrorMessage = ex.Message

                };
            }
        }

        public async Task<ServiceResponse<List<Product>>> ViewAllProductsSellerAsync(int sellerId)
        {
            try
            {
                var products = await stopAndShopDBContext.Products.Where(i => i.sellerId == sellerId).ToListAsync();

                return new ServiceResponse<List<Product>>()
                {
                    Data = products,
                    ResultMessage = products.Count == 0 ?  "No products found, please add product!" : "Found all your products",
                    Success = true,
                    
                };
            }catch(Exception ex)
            {
                return new ServiceResponse<List<Product>>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "No products found try again!"
                };
            }
        }

        public async Task<ServiceResponse<List<Product>>> FilterViewProductsUser(string productName, string category, string brand, int sellerId)
        {
            try
            {

                List<Product> products = await stopAndShopDBContext.Products.Where(i => 
                            i.productName.Contains(productName) ||
                            i.brand.Contains(brand) ||
                            i.category.Contains(category) ||
                            i.sellerId == sellerId).ToListAsync();



                return new ServiceResponse<List<Product>>()
                {
                    Data = products,
                    Success = true,
                    ResultMessage = "Found your interested products"
                };
                

            }catch(Exception ex)
            {
                return new ServiceResponse<List<Product>>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Some error occured, please try again later!"
                };

            }
        }

        public async Task<ServiceResponse<List<Product>>> FindAllProductsUser()
        {
            try
            {
                List<Product> products = await stopAndShopDBContext.Products.ToListAsync();

                return new ServiceResponse<List<Product>>()
                {
                    Data = products,
                    Success = true,
                    ResultMessage = "Here is all your products"
                };
            }catch (Exception ex)
            {
                return new ServiceResponse<List<Product>>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "There is some error finding products, try again!"
                };
            }
        }
        public bool ValidateProductExixts(Product product)
        {
            bool productExists = stopAndShopDBContext.Products.Any(p => 
                p.productName == product.productName &&
                p.brand == product.brand && 
                p.category == product.category && 
                p.size == product.size &&
                p.sellerId == product.sellerId
            );

            return productExists;
        }

        public bool ValidateSeller(int sellerId)
        {
            return stopAndShopDBContext.Sellers.Any(s => s.sellerId == sellerId);
        }

    }
}
