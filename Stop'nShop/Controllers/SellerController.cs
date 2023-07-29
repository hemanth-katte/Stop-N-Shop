using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Hubs;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;
using Stop_nShop.Service;


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
        public readonly IHubContext<BroadcastHub,IBroadcastHubClient> hubContext;

        public SellerController(ISellerService sellerService, IProductService productService,IInterestedService interestedService,IOrderService orderService,IHubContext<BroadcastHub,IBroadcastHubClient> hubContext)
        {
            _sellerService = sellerService;
            _productService = productService;
            this.interestedService = interestedService;
            this.orderService = orderService;
            this.hubContext = hubContext;    
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

        //add new product
        [HttpPost("addProduct")]

        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productRequestDto)
        {
            var result = await _productService.AddProductAsync(productRequestDto);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
            
        }


        //delete product
        [HttpDelete("{productId}")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProductAsync(int productId)
        {
            var response = await _productService.DeleteProductAsync(productId);

            if (!response.Success)
            {
                return BadRequest(response); 
            }

            return Ok(response); 
        }

        //add to interested list(wishlist/cartlist)

       // [HttpPost("addToInterested")]
        //public async Task<IActionResult> AddToInterested([FromBody]InterestedRequestDto interestedRequestDto)
        //{
        //    var response = await interestedService.AddToInterestedAsync(interestedRequestDto);

        //    if (response.Success)
        //        return Ok(response);
        //    return BadRequest(response);
        //}

        //fetch all products of seller
        [HttpGet("findAllProductsSeller")]
        public async Task<IActionResult> FindAllProductsSeller([FromQuery] int sellerId)
        {
            var response = await _productService.ViewAllProductsSellerAsync(sellerId);

            if(response.Success)
                return Ok(response);
            return BadRequest(response);
        }


        //fetch all orders for the seller
        [HttpGet("findAllOrdersPlaced")]
        public async Task<IActionResult> FindAllOrdersSeller([FromQuery] int sellerId)
        {
            var response = await orderService.FetchAllOrdersSellerAsync(sellerId);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        //process the new orders placed 
        [HttpPut("processOrders")]
        public async Task<IActionResult> ProcessNewOrdersAsync([FromQuery]int sellerId, [FromQuery] int orderId)
        {
            var response = await orderService.ProcessNewOrderAsync(sellerId, orderId);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);   
        }

        //show new orders placed for seller
        [HttpGet("getNewOrders")]
        public async Task<IActionResult> ShowNewOrdersPlacedSeller([FromQuery] int sellerId, [FromQuery] OrderStatus orderStatus)
        {
            var response = await orderService.ShowNewOrdersPlaced(sellerId, orderStatus);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        //broadcast message to all users
        [HttpPost("boradcastMessage")]
        public async Task<IActionResult> BroadcastMessage([FromQuery] List<string> offers)
        {
            var resposne = await hubContext.Clients.All.BroadcastOffersToUsers(offers);

            if(resposne.Equals("Offers sent to all users!"))
                return Ok(resposne);
            return BadRequest(resposne);
        }


    }
}
