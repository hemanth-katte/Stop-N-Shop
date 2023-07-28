using Microsoft.Identity.Client;
using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;
using Stop_nShop.Repository;

namespace Stop_nShop.Service
{

    public class OrderService : IOrderService
    {
        public readonly IOrderRepository orderRepository;
        public readonly IProductRepository productRepository;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }


        public async Task<ServiceResponse<PlaceOrderResponse>> PlaceOrderAsync(PlaceOrderDto placeOrderDto)
        {
            int productId = placeOrderDto.productId;
            ServiceResponse<Product> result = await productRepository.FindProductById(productId);

            if (!result.Success)
            {
                throw new Exception(result.ErrorMessage);
            }

            if (placeOrderDto.quantity > result.Data.quantity)
            {
                throw new Exception("Requested number of products not available");
            }

            List<Product> products = new List<Product>();
            Orders order = new Orders()
            {
                orderDate = DateTime.Now,
                products = products,
                sellerId = placeOrderDto.sellerId,
                userId = placeOrderDto.userId,
                orderStatus = Models.Enums.OrderStatus.BOOKED,
                totalPrice = placeOrderDto.price,
                quantity = placeOrderDto.quantity

            };

            var response = await orderRepository.PlaceOrderAsync(order);

            var serviceResponse = new ServiceResponse<PlaceOrderResponse>()
            {
                Data = response.Success ? new PlaceOrderResponse()
                {
                    userId = response.Data.userId,
                    sellerId = response.Data.sellerId,
                    orderId = response.Data.orderId,
                    quantity = placeOrderDto.quantity,
                    status = response.Data.orderStatus
                } : null,
                Success = response.Success,
                ResultMessage = response.ResultMessage
            };

            return serviceResponse;


        }

        public async Task<ServiceResponse<bool>> CancelOrderAsync(int orderId)
        {
            var result = await orderRepository.CancelOrderAsync(orderId);

            var response = new ServiceResponse<bool>()
            {
                Data = result.Data,
                Success = result.Success,
                ErrorMessage = result.ErrorMessage,
                ResultMessage = result.ResultMessage
            };

            return response;
        }

        public async Task<ServiceResponse<List<FetchOrderResponseDto>>> FetchAllOrdersAsync(int userId)
        {
            var result = await orderRepository.FetchAllOrdersAsync(userId);

            var ordersResponses = new List<FetchOrderResponseDto>();

            if (result.Success)
            {
                result.Data.ForEach(d =>
                {
                    FetchOrderResponseDto fetchOrder = new FetchOrderResponseDto()
                    {
                        orderId = d.orderId,
                        orderDateTime = d.orderDate,
                        orderStatus = d.orderStatus,
                        userId = d.userId,
                        totalPrice = d.totalPrice
                    };
                    ordersResponses.Add(fetchOrder);
                });
            }

            var response = new ServiceResponse<List<FetchOrderResponseDto>>()
            {
                Data = result.Success ? ordersResponses : null,
                Success = result.Success,
                ResultMessage = result.ResultMessage,
                ErrorMessage = result.ErrorMessage
            };

            return response;
        }

        public async Task<ServiceResponse<List<Orders>>> FetchAllOrdersSellerAsync(int sellerId)
        {

            var response = await orderRepository.FetchAllOrdersSellerAsync(sellerId);

            return response;
           
        }

        public async Task<ServiceResponse<bool>> ProcessNewOrderAsync(int sellerId, int orderId)
        {
            var response = await orderRepository.ProcessNewOrdersAsync(sellerId, orderId);

            return response;
        }

        public async Task<ServiceResponse<List<Orders>>> ShowNewOrdersPlaced(int sellerId, OrderStatus status)
        {
            var response = await orderRepository.ShowNewOrdersPlaced(sellerId,status);

            return response;
        }
    }
}

