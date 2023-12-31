﻿using MailKit.Security;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using System.IdentityModel.Tokens.Jwt;
using MailKit.Net.Smtp;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.DataProtection;
using Stop_nShop.Service.ServiceInterface;
using Stop_nShop.Repository.RepositoryInterface;
using Stop_nShop.Models.Responses;
using Stop_nShop.Models.Enums;
using System.Text.RegularExpressions;

namespace Stop_nShop.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        private IConfiguration configuration;
        private readonly IDataProtector dataProtector;
        public UserService(IUserRepository _userRepository, IConfiguration configuration,IDataProtectionProvider dataProtectionProvider)
        {
            userRepository = _userRepository;
            this.configuration = configuration;
            dataProtector = dataProtectionProvider.CreateProtector("PasswordProtection");
        }

        public async Task<ServiceResponse<UserResponseDto>> AddUserAsync(UserRequestDto userRequestDto)
        {

            if (userRequestDto.firstName.Any(char.IsDigit) || userRequestDto.lastName.Any(char.IsDigit))
            {
                return new ServiceResponse<UserResponseDto>
                {
                    Data = null,
                    ResultMessage = "Enter valid firstname and lastname",
                    ErrorMessage = "Please do not include digits in firstname or lastname!",
                    Success = false
                };
            }

            Regex passwordRegex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");

            if (!passwordRegex.IsMatch(userRequestDto.password))
            {
                return new ServiceResponse<UserResponseDto>()
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

            if (!emailRegex.IsMatch(userRequestDto.email))
            {
                return new ServiceResponse<UserResponseDto>()
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = "Not a valid email id",
                    ResultMessage = "Please enter a valid email id"
                };
            }

            Regex phoneRegex = new Regex(@"^[789]\d{9}$");

            if (phoneRegex.IsMatch(userRequestDto.phone))
            {
                return new ServiceResponse<UserResponseDto>()
                {
                    Success = false,
                    Data = null,
                    ErrorMessage = "Phone number not valid",
                    ResultMessage = "Please enter valid phone number"
                };
            }

            var user = new User
            {
                firstName = userRequestDto.firstName,
                lastName = userRequestDto.lastName,
                userEmail = userRequestDto.email,
                userAddress = userRequestDto.address,
                userPhone = userRequestDto.phone,
                userPassword = EncryptPassword(userRequestDto.password),
                userStatus = UserStatus.Active,
            };

            var result = await userRepository.AddUserAsync(user);

            var response = new ServiceResponse<UserResponseDto>
            {
                Data = result.Success ? new UserResponseDto
                {
                    firstName = result.Data.firstName,
                    lastName = result.Data.lastName,
                    userEmail = result.Data.userEmail,
                    userAddress = result.Data.userAddress,
                    userPhone = result.Data.userPhone,

                } : null,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage,
                ResultMessage = result.ResultMessage
            };

            return response;
        }


        public async Task<ServiceResponse<UserResponseDto>> FindUserByNameAsync(string userName)
        {
            var result = await userRepository.FindUserByNameAsync(userName);

            var response = new ServiceResponse<UserResponseDto>
            {
                Data = result.Success ? new UserResponseDto
                {
                    userId = result.Data.userId,
                    firstName = result.Data.firstName,
                    lastName = result.Data.lastName,
                    userEmail = result.Data.userEmail,
                    userAddress = result.Data.userAddress,
                    userPhone = result.Data.userPhone,
                    userStatus = result.Data.userStatus,

                } : null,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage
            };

            return response;
        }

        public async Task<ServiceResponse<string>> GetToken(UserLoginDto loginDto)
        {
            
            var result = await userRepository.AuthenticateUser(loginDto);

            if (result.Success)
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, result.Data.userId.ToString())
                };

                var token = new JwtSecurityToken
                (   issuer: configuration["Jwt:Issuer"],
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
                    UserId = result.Data.userId,
                    UserName = result.Data.firstName
                    
                };
            }

            return new ServiceResponse<string> 
            { 
                Success = false,
                Data = null,
                ResultMessage = result.ResultMessage,
                ErrorMessage = result.ErrorMessage
            };
        }

        public async Task<ServiceResponse<bool>> SendEmail(SendEmailDto emailDto)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration["Email:UserName"]));
            email.To.Add(MailboxAddress.Parse(emailDto.To));
            email.Subject = emailDto.Subject;
            email.Body = new TextPart(TextFormat.Text) { Text = emailDto.Body};

            var smtpClient = new SmtpClient();
            smtpClient.Connect(configuration["Email:Host"], 587, SecureSocketOptions.StartTls);
            smtpClient.Authenticate(configuration["Email:UserName"], configuration["Email:Password"]);
            try
            {
                smtpClient.Send(email);
                smtpClient.Disconnect(true);
                return new ServiceResponse<bool> 
                { 
                    Success = true,
                    Data = true,
                    ResultMessage = "Email successfully sent!" 
                };
            }catch (Exception ex)
            {
                return new ServiceResponse<bool>()
                {
                    Success = false,
                    Data = false,
                    ResultMessage = "Please try again",
                    ErrorMessage = ex.Message,
                };
            }

           
           
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

        public async Task<ServiceResponse<string>> GetPassword(int userId)
        {
            var response = await userRepository.GetPassword(userId);

            return response;
        }


    }
}
