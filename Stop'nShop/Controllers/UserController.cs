﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Repository;
using Stop_nShop.Service;
using System.Security.Claims;

namespace Stop_nShop.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IOrderService orderService;
        private readonly IInterestedService interestedService;
        private readonly IProductService productService;
       // private IConfiguration configuration;
        public UserController(IUserService _userService,IOrderService orderService,IInterestedService interestedService, IProductService productService)
        {
            userService = _userService;
            this.orderService = orderService;
            this.interestedService = interestedService;
            this.productService = productService;
           // this.configuration = configuration;
        }

        //add new user
        [HttpPost("addUser")]
        public async Task<IActionResult> AddUser([FromBody] UserRequestDto userRequestDto)
        {
            var result = await userService.AddUserAsync(userRequestDto);

            if(result.Success) 
                return Ok(result); 
            return BadRequest(result);
        }

        //Login User Generate token
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginDto loginDto)
        {
            IActionResult response = Unauthorized();

            var result = await userService.GetToken(loginDto);
            if (result.Success)
            {
                response = Ok(result);
            }
            return response;
        }

        //get user by name
        [HttpGet("findByName")]
        public async Task<IActionResult> FindUserByName([FromQuery] string userName)
        {
            var result = await userService.FindUserByNameAsync(userName);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);

        }

        //place order
        [HttpPost("placeOrder")]
        public async Task<IActionResult> PlaceOrder([FromBody] PlaceOrderDto placeOrderDto)
        {
            var result = await orderService.PlaceOrderAsync(placeOrderDto);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        //cancel order
        [Authorize]
        [HttpPut("cancelOrder")]
        public async Task<IActionResult> CancelOrder([FromQuery] int orderId)

        { 
            var result = await orderService.CancelOrderAsync(orderId);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        
        //get wishList/cartList
        [HttpGet("getInterested")]
        public async Task<IActionResult> GetALlInterested([FromQuery] int userId, [FromQuery] InterestedStatus status)
        {
            var result = await interestedService.GetAllInterestedAsync(userId,status);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }


        //fetch all orders 
        [HttpGet("fetchAllOrders")]
        public async Task<IActionResult> FetchAllOrders([FromQuery] int userId)
        {
            var result = await orderService.FetchAllOrdersAsync(userId);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        //get products category/brand/namewise
        [HttpGet("findProductsFilter")]
        public async Task<IActionResult> FindFilterProducts(string productName, string category, string brand, int sellerId)
        {
            var result = await productService.FilterViewProductsUser(productName, category, brand, sellerId);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        //add to interested list(wishlist/cartlist)
        [HttpPost("addToInterested")]
        public async Task<IActionResult> AddToInterested([FromBody] InterestedRequestDto interestedRequestDto)
        {
            var response = await interestedService.AddToInterestedAsync(interestedRequestDto);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        //send email to seller 
        [HttpPost("sendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] SendEmailDto emailDto)
        {
            var response = await userService.SendEmail(emailDto);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

    }
}
