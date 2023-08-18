using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;
using Stop_nShop.Service;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InterestedController : ControllerBase
    {

        private readonly IInterestedService interestedService;

        public InterestedController(IInterestedService interestedService)
        {
            this.interestedService = interestedService;
        }

        /// <summary>
        /// Add to wishlist or cartlist by customer 
        /// </summary>
        /// <param name="interestedRequestDto"></param>
        /// <returns></returns>
        //add to interested list(wishlist/cartlist)
        [HttpPost("addToInterested")]
        public async Task<IActionResult> AddToInterested([FromBody] InterestedRequestDto interestedRequestDto)
        {
            if(HttpContext.Request.Headers.TryGetValue("UserID",out var userIdHeader) &&
                int.TryParse(userIdHeader.ToString(),out int userId)) 
            {
                interestedRequestDto.userId = userId;
            }
            else
            {
                return BadRequest(new ServiceResponse<bool>()
                {
                    ResultMessage = "User not found please login once again!",
                    Data = false
                });
            }

            var response = await interestedService.AddToInterestedAsync(interestedRequestDto);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }


        /// <summary>
        /// Get wishlist items or cartlist items based on the customer request
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        //get wishList/cartList
        [HttpGet("getInterested")]
        public async Task<IActionResult> GetALlInterested([FromQuery] int userId, [FromQuery] InterestedStatus status)
        {
            var result = await interestedService.GetAllInterestedAsync(userId, status);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
    }
}
