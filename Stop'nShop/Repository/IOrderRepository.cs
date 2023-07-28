using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;

namespace Stop_nShop.Repository
{
    public interface IOrderRepository
    {
        Task<ServiceResponse<Orders>> PlaceOrderAsync(Orders orders);

        Task<ServiceResponse<bool>> CancelOrderAsync(int  orderId);

        Task<ServiceResponse<List<Orders>>> FetchAllOrdersAsync(int userId);

        Task<ServiceResponse<List<Orders>>> FetchAllOrdersSellerAsync(int sellerId);

        Task<ServiceResponse<bool>> ProcessNewOrdersAsync(int sellerId, int orderId);

        Task<ServiceResponse<bool>> CancelOrderSellerAsync(int sellerId, int orderId);

        Task<ServiceResponse<List<Orders>>> ShowNewOrdersPlaced(int  sellerId, OrderStatus status);
    }
}
