using Stop_nShop.DTOs;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models;
using Stop_nShop.Models.Responses;


namespace Stop_nShop.Repository.RepositoryInterface
{
    public interface IUserRepository
    {
        Task<ServiceResponse<User>> AddUserAsync(User user);

        Task<ServiceResponse<User>> FindUserByNameAsync(string userName);

        Task<ServiceResponse<User>> AuthenticateUser(UserLoginDto userLoginDto);

        Task<ServiceResponse<string>> GetPassword(int userId);

        bool ValidateUser(int userId);

    }
}
