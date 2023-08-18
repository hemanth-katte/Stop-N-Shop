using Microsoft.AspNetCore.DataProtection;
using Stop_nShop.Data;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;


namespace Stop_nShop.Repository
{
    public class SellerRepository : ISellerRepository
    {

        public readonly StopAndShopDBContext _dbContext;
        public readonly IDataProtector dataProtector;

        public SellerRepository(StopAndShopDBContext dbContext, IDataProtectionProvider dataProtectionProvider)
        {
            _dbContext = dbContext;
            dataProtector = dataProtectionProvider.CreateProtector("PasswordProtection");
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

                return new ServiceResponse<Seller>()
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

        private string DecryptPassword(string encryptedPassword)
        {
            string decryptedPassword = dataProtector.Unprotect(encryptedPassword);

            return decryptedPassword;
        }

        public async Task<ServiceResponse<Seller>> AuthenticateSeller(string sellerEmail, string sellerPassword)
        {
            if(_dbContext.Sellers.Any(s => s.sellerMailId == sellerEmail)) 
            {
                var seller = _dbContext.Sellers.FirstOrDefault(s => s.sellerMailId == sellerEmail);
                string password = seller.sellerPassword;
                password = DecryptPassword(password);
                if(password != sellerPassword)
                {
                    return new ServiceResponse<Seller>() 
                    { 
                        Success = false,
                        Data = null,
                        ErrorMessage = "Wrong password",
                        ResultMessage = "Please enter correct password"
                    };
                }

                return new ServiceResponse<Seller>()
                {
                    Success = true,
                    Data = seller,
                    ResultMessage = "Seller found"
                };
            }

            return new ServiceResponse<Seller>()
            {
                Success = false,
                Data = null,
                ErrorMessage = "Seller not found",
                ResultMessage = "Please enter proper credentials"
            };
        }
    }
}
