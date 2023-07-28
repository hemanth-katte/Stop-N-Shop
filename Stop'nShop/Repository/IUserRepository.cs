using Stop_nShop.DTOs;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository
{
    public interface IUserRepository
    {
        Task<ServiceResponse<User>> AddUserAsync(User user);

        Task<ServiceResponse<User>> FindUserByNameAsync(string userName);

        Task<ServiceResponse<User>> AuthenticateUser(UserLoginDto userLoginDto);


        bool ValidateUser(int userId);

    }
}
