using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models.Enums;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IOrderService orderService;
       
        
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }


        //place order
        /// <summary>
        /// Customer can place order containing any number of products
        /// </summary>
        /// <param name="placeOrderDto"></param>
        /// <returns></returns>
        [HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto placeOrderDto)
        {
            var result = await orderService.PlaceOrderAsync(placeOrderDto);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Cancel order by customer 
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("cancelOrder")]
        public async Task<IActionResult> CancelOrder([FromQuery] int orderId)

        {
            var result = await orderService.CancelOrderAsync(orderId);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        //fetch all orders 
        /// <summary>
        /// Fetch all orders placed by the customer, that are active, cancelled, delivered
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("fetchAllOrders")]
        public async Task<IActionResult> FetchAllOrders([FromQuery] int userId)
        {
            var result = await orderService.FetchAllOrdersAsync(userId);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        /// <summary>
        /// Fetch all the orders placeed for the seller including active, delivered, cancelled
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        //fetch all orders for the seller
        [HttpGet("findAllOrdersPlaced")]
        public async Task<IActionResult> FindAllOrdersSeller([FromQuery] int sellerId)
        {
            var response = await orderService.FetchAllOrdersSellerAsync(sellerId);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// To process the new orders that are placed for the seller either ship or cancel
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="orderId"></param>
        /// <returns>  </returns>
        [HttpPut("processOrders")]
        public async Task<IActionResult> ProcessNewOrdersAsync([FromQuery] int sellerId, [FromQuery] int orderId)
        {
            var response = await orderService.ProcessNewOrderAsync(sellerId, orderId);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Show all the new active orders that need to be processed
        /// </summary>
        /// <param name="sellerId"></param>
        /// <param name="orderStatus"></param>
        /// <returns></returns>
        //show new orders placed for seller
        [HttpGet("getNewOrders")]
        public async Task<IActionResult> ShowNewOrdersPlacedSeller([FromQuery] int sellerId, [FromQuery] OrderStatus orderStatus)
        {
            var response = await orderService.ShowNewOrdersPlaced(sellerId, orderStatus);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
