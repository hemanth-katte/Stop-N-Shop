using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using Stop_nShop.Data;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;

namespace Stop_nShop.Repository


{
    public class UserRepository : IUserRepository
    {
        public readonly StopAndShopDBContext _dbContext;
        private readonly IDataProtector dataProtector;
        public UserRepository(StopAndShopDBContext dbContext, IDataProtectionProvider dataProtectionProvider)
        {
            _dbContext = dbContext;
            dataProtector = dataProtectionProvider.CreateProtector("PasswordProtection");
        }

        public async Task<ServiceResponse<User>> AddUserAsync(User user)
        {
            try
            {
                bool validate = Validate(user.userEmail, user.userPhone);

                if (!validate)
                {
                    return new ServiceResponse<User>
                    {
                        Success = false,
                        Data = null,
                        ErrorMessage = "User already exists",
                        ResultMessage = "Try with a different mail id"
                    };

                }
                    

                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return new ServiceResponse<User>
                {
                    Success = true,
                    Data = user,
                    ResultMessage = "User added successfully",
                    ErrorMessage = "None"
                };
            }
            catch (DbUpdateException ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Error occured while storing to database"
                };
            }
        }

        public async Task<ServiceResponse<User>> FindUserByNameAsync(string userName)
        {

            var response = new ServiceResponse<User>();

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.firstName == userName);

                if(user == null)
                {
                    response.Success = false;
                    response.ErrorMessage = "User not found";
                    return response;
                }

                response.Data = user;
                response.Success = true;
            }
            catch (DbUpdateException ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }

            return response;

        }

        public bool Validate(string email,string phoneNumber)
        {
            return !_dbContext.Users.Any(u => u.userEmail == email) && !_dbContext.Users.Any(u => u.userPhone == phoneNumber);
        }
    
        public bool ValidateUser(int userId)
        {
            return _dbContext.Users.Any(i => i.userId == userId);

        }

        private string DecryptPassword(string encryptedPassword)
        {
            string decryptedPassword = dataProtector.Unprotect(encryptedPassword);

            return decryptedPassword;
        }
        public async Task<ServiceResponse<User>> AuthenticateUser(UserLoginDto user)
        {
            try
            {
                User user1 = null;
                
                //if (_dbContext.Users.Any(u => u.userEmail == user.userEmail && u.userPassword == user.password))
                //{
                //    user1 = _dbContext.Users.FirstOrDefault(u => u.userEmail == user.userEmail);
                //}

                user1 = _dbContext.Users.FirstOrDefault(u => u.userEmail == user.userEmail);
                var password = user1.userPassword;
                password = DecryptPassword(password);
                if(password != user.password) 
                {
                    return new ServiceResponse<User>
                    {
                        Success = false,
                        ResultMessage = "Please enter proper credentials!",
                        ErrorMessage = "UserEmail or password does not match"
                    };
                }

                if (user1 != null)
                {
                    return new ServiceResponse<User>
                    {
                        Success = true,
                        Data = user1,
                        ResultMessage = "Valid user found"
                    };
                }

                return new ServiceResponse<User> 
                { 
                    Success = false,
                    ResultMessage = "Please enter proper credentials!",
                    ErrorMessage = "UserEmail or password does not match"
                };


            }catch (Exception ex)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Some error occured please try after sometime!"
                };
            }
        }

        public async Task<ServiceResponse<string>> GetPassword(int userId)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.userId == userId);

            var password = user.userPassword;
            password = DecryptPassword(password);

            return new ServiceResponse<string>()
            {
                Data = password,
                Success = true,
            };
        }
    }
}
