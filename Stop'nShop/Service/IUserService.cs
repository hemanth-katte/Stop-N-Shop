﻿using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Service
{
    public interface IUserService
    {
        Task<ServiceResponse<UserResponseDto>> AddUserAsync(UserRequestDto userRequestDto);

        Task<ServiceResponse<UserResponseDto>> FindUserByNameAsync(string userName);

        Task<ServiceResponse<string>> GetToken(UserLoginDto loginDto);

        Task<ServiceResponse<bool>> SendEmail(SendEmailDto sendEmailDto);

    }
}