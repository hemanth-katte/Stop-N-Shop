using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository
{
    public interface IInterestedRepository
    {
        Task<ServiceResponse<Interested>> AddToInterestedAsync(Interested interested);

        Task<ServiceResponse<ViewInterestedResponseDto>> GetAllInterestedAsync(int userId, InterestedStatus status);
        
    }
}
