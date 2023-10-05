using Microsoft.AspNetCore.DataProtection;
using Microsoft.IdentityModel.Tokens;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;
using Stop_nShop.Service.ServiceInterface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.RegularExpressions;

namespace Stop_nShop.Service
{
    public class SellerService : ISellerService
    {
        public readonly ISellerRepository _sellerRepository;
        public readonly IDataProtector dataProtector;
        public readonly IConfiguration configuration;
        public SellerService(ISellerRepository sellerRepository,IDataProtectionProvider dataProtectionProvider, IConfiguration configuration)
        {
            _sellerRepository = sellerRepository;
            dataProtector = dataProtectionProvider.CreateProtector("PasswordProtection");
            this.configuration = configuration;
        }

        public async Task<ServiceResponse<SellerRequestDto>> AddSellerAsync(SellerRequestDto sellerRequestDto)
        {
            if (sellerRequestDto.sellerName.Any(char.IsDigit))
            {
                return new ServiceResponse<SellerRequestDto>
                {
                    Data = null,
                    ResultMessage = "Enter valid firstname and lastname",
                    ErrorMessage = "Please do not include digits in firstname or lastname!",
                    Success = false
                };
            }

            Regex passwordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (!passwordRegex.IsMatch(sellerRequestDto.sellerPassword))
            {
                return new ServiceResponse<SellerRequestDto>()
                {
                    Success = false,
                    Data = null,
                    ResultMessage = "Please enter a password that " +
                    "contains minimum of 8 characteres, at least 1 digit, at least 1 special character, " +
                    "at least 1 uppercase at least 1 lowercase",
                    ErrorMessage = "Password doesn't match requirements"
                    
                };
            }

            Regex emailRegex = new Regex("^\\S+@\\S+\\.\\S+$");

            if (!emailRegex.IsMatch(sellerRequestDto.sellerEmail))
            {
                return new ServiceResponse<SellerRequestDto>()
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = "Not a valid email id",
                    ResultMessage = "Please enter a valid email id"
                };
            }

            Regex phoneRegex = new Regex(@"^[789]\d{9}$");

            if (phoneRegex.IsMatch(sellerRequestDto.sellerPhone))
            {
                return new ServiceResponse<SellerRequestDto>()
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = "Phone number not valid",
                    ResultMessage = "Please enter valid phone number"
                };
            }

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

        public async Task<ServiceResponse<string>> GenerateToken(string sellerEmail, string sellerPassword)
        {
            var result = await _sellerRepository.AuthenticateSeller(sellerEmail, sellerPassword);

            if (result.Success)
            {

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Data.sellerId.ToString())
                };

                var token = new JwtSecurityToken
                (issuer: configuration["Jwt:Issuer"],
                    audience: configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(20),
                    signingCredentials: credentials
                );

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

                return new ServiceResponse<string>
                {
                    Success = true,
                    Data = tokenString,
                    ResultMessage = "Here is your security token for the next 20 minutes",
                    UserId = result.Data.sellerId,
                    UserName = result.Data.sellerName

                };
            }

            return new ServiceResponse<string>()
            {
                Success = false,
                ErrorMessage = result.ErrorMessage,
                ResultMessage = result.ResultMessage,
                Data = null

            };
        
        }
    }
}
