using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Controllers
{
    [ApiController]
    [Route("api/sellers")]
    public class SellerController : ControllerBase
    {

        public readonly ISellerService _sellerService;
        public readonly IProductService _productService;
        public readonly IInterestedService interestedService;
        public readonly IOrderService orderService;
       
        public SellerController(ISellerService sellerService, IProductService productService,IInterestedService interestedService,IOrderService orderService) 
        {
            _sellerService = sellerService;
            _productService = productService;
            this.interestedService = interestedService;
            this.orderService = orderService;

        }

        //add new seller
        [HttpPost("addSeller")]
        public async Task<IActionResult> AddSeller([FromBody] SellerRequestDto sellerRequestDto)
        {
            var result = await _sellerService.AddSellerAsync(sellerRequestDto);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);

        }

        //login seller
        [AllowAnonymous]
        [HttpPost("loginSeller")]
        public async Task<IActionResult> LoginSeller(string sellerEmail, string sellerPassword)
        {
            var response = await _sellerService.GenerateToken(sellerEmail, sellerPassword);

            if(response.Success) return Ok(response);
            return BadRequest(response);
        }



    }
}
