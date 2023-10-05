using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.Models.Responses;
using Stop_nShop.Service;
using Stop_nShop.Service.ServiceInterface;

namespace Stop_nShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        /// <summary>
        /// Add new products by a user registered as seller 
        /// </summary>
        /// <param name="productRequestDto"></param>
        /// <returns></returns>
        //add new product
        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] ProductRequestDto productRequestDto)
        {
            var result = await productService.AddProductAsync(productRequestDto);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);

        }

        /// <summary>
        /// Delete products that are added by the seller by the same seller 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        //delete product
        [HttpDelete("deleteProduct")]
        public async Task<ActionResult<ServiceResponse<bool>>> DeleteProductAsync(int productId)
        {
            var response = await productService.DeleteProductAsync(productId);

            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Fetch all products of the particular seller that he has added
        /// </summary>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        //fetch all products of seller
        [HttpGet("findAllProductsSeller")]
        public async Task<IActionResult> FindAllProductsSeller([FromQuery] int sellerId)
        {
            var response = await productService.ViewAllProductsSellerAsync(sellerId);

            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }

        /// <summary>
        /// Get products based on category as per user request 
        /// </summary>
        /// <param name="productName"></param>
        /// <param name="category"></param>
        /// <param name="brand"></param>
        /// <param name="sellerId"></param>
        /// <returns></returns>
        //get products category/brand/namewise
        [HttpGet("findProductsFilter")]
        public async Task<IActionResult> FindFilterProducts(string productName, string category, string brand, int sellerId)
        {
            var result = await productService.FilterViewProductsUser(productName, category, brand, sellerId);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("fetchAllTheProducts")]
        public async Task<IActionResult> FetchAllTheProducts()
        {
            var result = await productService.FetchAllTheProducts();

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }



    }
}
