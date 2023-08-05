using Stop_nShop.Data;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;


namespace Stop_nShop.Repository
{
    public class SellerRepository : ISellerRepository
    {

        public readonly StopAndShopDBContext _dbContext;

        public SellerRepository(StopAndShopDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ServiceResponse<Seller>> AddSellerAsync(Seller seller)
        {
            try
            {

                bool validate = Validate(seller.sellerMailId, seller.sellerPhone);

                if(!validate)
                {
                    return new ServiceResponse<Seller>()
                    {
                        Data = null,
                        Success = false,
                        ErrorMessage = "Seller already exixts!",
                        ResultMessage = "Please login!"
                    };
                }

                await _dbContext.Sellers.AddAsync(seller);
                await _dbContext.SaveChangesAsync();

                return new ServiceResponse<Seller>
                {
                    Data = seller,
                    Success = true,
                    ResultMessage = "Seller added successfully"
                };

            }catch (Exception ex)
            {
                return new ServiceResponse<Seller>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Please try again later!"
                };
            }
        }

        public bool Validate(string email,  string phonenumber)
        {
            return !_dbContext.Sellers.Any(u => u.sellerMailId == email) && !_dbContext.Sellers.Any(u => u.sellerPhone == phonenumber); 
        }
    }
}
