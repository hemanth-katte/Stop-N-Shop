using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Service
{
    public class InterestedService : IInterestedService
    {
        public readonly IInterestedRepository interestedRepository;
        public readonly IProductRepository productRepository;
        public readonly IUserRepository userRepository;

        public InterestedService(IInterestedRepository interestedRepository,IProductRepository productRepository,IUserRepository userRepository)
        {
            this.interestedRepository = interestedRepository;
            this.productRepository = productRepository;
            this.userRepository = userRepository;
        }

        public async Task<ServiceResponse<InterestedResponseDto>> AddToInterestedAsync(InterestedRequestDto interestedRequestDto)
        {
            var pro = await productRepository.FindProductById(interestedRequestDto.productId);
            Product product1 = pro.Data;

            Interested interested = new Interested()
            {
                userId = interestedRequestDto.userId,
                productId = interestedRequestDto.productId,
                sellerId = interestedRequestDto.sellerId,
                product = product1,
                status = interestedRequestDto.status
             
            };

            var result = await interestedRepository.AddToInterestedAsync(interested);

            var resposne = new ServiceResponse<InterestedResponseDto>()
            {
                Data = result.Success ? new InterestedResponseDto()
                {
                    interestedId = result.Data.interestedID,
                    userId = result.Data.userId,
                    sellerId = result.Data.sellerId,
                    productId = result.Data.productId,
                    status = result.Data.status
                } : null,
                Success = result.Success,
                ResultMessage = result.ResultMessage,
                ErrorMessage = result.ErrorMessage
                
            };

            return resposne;

        }

        public async Task<ServiceResponse<ViewInterestedResponseDto>> GetAllInterestedAsync(int userId, InterestedStatus status)
        {

            bool existing = userRepository.ValidateUser(userId);

            if (!existing)
            {
                throw new Exception("User not found, please register user!");
            }

            var result = await interestedRepository.GetAllInterestedAsync(userId, status);

            var response = new ServiceResponse<ViewInterestedResponseDto>()
            {
                Data = result.Success ? result.Data : null,
                Success = result.Success,
                ResultMessage = result.ResultMessage,
                ErrorMessage = result.ErrorMessage
            };

            return response;
        }
    }
}
