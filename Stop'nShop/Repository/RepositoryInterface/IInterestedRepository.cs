﻿using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository.RepositoryInterface
{
    public interface IInterestedRepository
    {
        Task<ServiceResponse<Interested>> AddToInterestedAsync(Interested interested);

        Task<ServiceResponse<ViewInterestedResponseDto>> GetAllInterestedAsync(int userId, InterestedStatus status);

        Task<ServiceResponse<bool>> RemoveFromInterestedList(int userId, int productId);

    }
}
