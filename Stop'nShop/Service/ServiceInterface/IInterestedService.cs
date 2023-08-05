using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Service.ServiceInterface
{
    public interface IInterestedService
    {
        Task<ServiceResponse<InterestedResponseDto>> AddToInterestedAsync(InterestedRequestDto interestedRequestDto);

        Task<ServiceResponse<ViewInterestedResponseDto>> GetAllInterestedAsync(int userId, InterestedStatus status);

    }
}
