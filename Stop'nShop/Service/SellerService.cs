using Microsoft.AspNetCore.DataProtection;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Service
{
    public class SellerService : ISellerService
    {
        public readonly ISellerRepository _sellerRepository;
        public readonly IDataProtector dataProtector;
        public SellerService(ISellerRepository sellerRepository,IDataProtectionProvider dataProtectionProvider)
        {
            _sellerRepository = sellerRepository;
            dataProtector = dataProtectionProvider.CreateProtector("PasswordProtection");
        }

        public async Task<ServiceResponse<SellerRequestDto>> AddSellerAsync(SellerRequestDto sellerRequestDto)
        {
            Seller seller = new Seller()
            {
                sellerName = sellerRequestDto.sellerName,
                sellerAddress = sellerRequestDto.sellerAddress,
                sellerMailId = sellerRequestDto.sellerEmail,
                sellerPhone = sellerRequestDto.sellerPhone,
                sellerPassword = EncryptPassword(sellerRequestDto.sellerPassword),

            };

            var result = await _sellerRepository.AddSellerAsync(seller);

            var respose = new ServiceResponse<SellerRequestDto>
            {
                Data = result.Success ? new SellerRequestDto()
                {
                    sellerName = result.Data.sellerName,
                    sellerAddress = result.Data.sellerAddress,
                    sellerEmail = result.Data.sellerMailId,
                    sellerPhone = result.Data.sellerPhone,
                    
                } : null,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage,
                ResultMessage = result.ResultMessage
                
            };

            return respose;

        }

        private string EncryptPassword(string password)
        {
            string encryptedPassword = dataProtector.Protect(password);

            return encryptedPassword;
        }

        private string DecryptPassword(string encryptedPassword)
        {
            string decryptedPassword = dataProtector.Unprotect(encryptedPassword);

            return decryptedPassword;
        }
    }
}
