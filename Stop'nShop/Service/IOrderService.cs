using Stop_nShop.DTOs.RequestDTOs;
using Stop_nShop.DTOs.ResponseDto;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Service
{
    public interface IOrderService
    {

        Task<ServiceResponse<PlaceOrderResponse>> PlaceOrderAsync(PlaceOrderDto placeOrderDto);

        Task<ServiceResponse<bool>> CancelOrderAsync(int orderId);

        Task<ServiceResponse<List<FetchOrderResponseDto>>> FetchAllOrdersAsync(int userId);

        Task<ServiceResponse<List<Orders>>> FetchAllOrdersSellerAsync(int sellerId);

        Task<ServiceResponse<bool>> ProcessNewOrderAsync(int sellerId, int orderId);

        Task<ServiceResponse<List<Orders>>> ShowNewOrdersPlaced(int sellerId, OrderStatus status);
    }
}
