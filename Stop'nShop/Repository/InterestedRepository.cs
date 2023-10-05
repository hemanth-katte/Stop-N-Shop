using Microsoft.EntityFrameworkCore;
using Stop_nShop.Data;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository.RepositoryInterface;


namespace Stop_nShop.Repository
{
    public class InterestedRepository : IInterestedRepository
    {
        public readonly StopAndShopDBContext stopAndShopDBContext;

        public InterestedRepository(StopAndShopDBContext stopAndShopDBContext)
        {
            this.stopAndShopDBContext = stopAndShopDBContext;
        }

        public async Task<ServiceResponse<Interested>> AddToInterestedAsync(Interested interested)
        {
            try
            {
                bool existing = Existing(interested.productId, interested.userId, interested.sellerId);
                if(existing)
                {
                    Interested interested1 = await stopAndShopDBContext.Interested.
                                          FirstOrDefaultAsync(p => p.productId == interested.productId 
                                          && p.userId == interested.userId 
                                          && p.sellerId == interested.sellerId); 
                    interested1.status = interested.status;
                    await stopAndShopDBContext.SaveChangesAsync();
                    return new ServiceResponse<Interested>()
                    {
                        Success = true,
                        Data = interested,
                        ResultMessage = "Item added to your " + interested.status
                    };
                }

                await stopAndShopDBContext.Interested.AddAsync(interested);
                await stopAndShopDBContext.SaveChangesAsync();

                return new ServiceResponse<Interested>()
                {
                    Success = true,
                    Data = interested,
                    ResultMessage = "Item added to your " + interested.status
                };
            }catch (Exception ex)
            {
                return new ServiceResponse<Interested>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Item could not be added to your interested list, Try Again!"
                };
            }
            
        }

        public async Task<ServiceResponse<ViewInterestedResponseDto>> GetAllInterestedAsync(int userID, InterestedStatus status)
        {

            try
            {
                var products = await stopAndShopDBContext.Interested
                    .Where(i => i.userId == userID && i.status == status)
                    .Select(i => i.product).ToListAsync();

                ViewInterestedResponseDto viewInterestedResponseDto = new ViewInterestedResponseDto()
                {
                    userId = userID,
                    interestedList = products
                };

                return new ServiceResponse<ViewInterestedResponseDto>()
                {
                    Data = viewInterestedResponseDto,
                    Success = true,
                    ResultMessage = "Here is your" + status + "list"

                };

            }catch(Exception ex)
            {
                return new ServiceResponse<ViewInterestedResponseDto>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Error occured! try again later!"
                };

            }
        }

        public async Task<ServiceResponse<bool>> RemoveFromInterestedList(int userId, int productId)
        {
            if (stopAndShopDBContext.Interested.Any(i => i.userId == userId && i.productId == productId))
            {
                var interestedEntryToDelete = stopAndShopDBContext.Interested.FirstOrDefault(i => i.userId == userId && i.productId == productId);
                if (interestedEntryToDelete != null)
                {
                    stopAndShopDBContext.Interested.Remove(interestedEntryToDelete);
                    await stopAndShopDBContext.SaveChangesAsync();
                }

                return new ServiceResponse<bool>()
                {
                    Data = true,
                    Success = true,
                    ResultMessage = "Product removed from your list"
                };
            }
            return new ServiceResponse<bool>()
            {
                Success = false,
                Data = false,
                ResultMessage = "Please try again later",
                ErrorMessage = "There was an error finding the product"
            };
        }
        


        public bool Existing(int productId, int userId, int sellerId)
        {
            return stopAndShopDBContext.Interested.Any(i => i.productId == productId && i.userId == userId && i.sellerId == sellerId);
        }
    }
}
