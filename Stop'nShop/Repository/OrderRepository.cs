using Microsoft.EntityFrameworkCore;
using Stop_nShop.Data;
using Stop_nShop.Models;
using Stop_nShop.Models.Enums;
using Stop_nShop.Models.Responses;
using System.Reflection.Metadata.Ecma335;

namespace Stop_nShop.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public readonly StopAndShopDBContext stopAndShopDBContext;

        public OrderRepository(StopAndShopDBContext stopAndShopDBContext)
        {
            this.stopAndShopDBContext = stopAndShopDBContext;
        }


        public async Task<ServiceResponse<Orders>> PlaceOrderAsync(Orders order)
        {
            try
            {

                await stopAndShopDBContext.Orders.AddAsync(order);

                Product product = await stopAndShopDBContext.Products.FirstOrDefaultAsync(p => p.productId == order.products.First().productId);

                product.quantity = product.quantity - order.quantity;

                await stopAndShopDBContext.SaveChangesAsync();



                return new ServiceResponse<Orders>
                {
                    Success = true,
                    Data = order,
                    ResultMessage = "Order placed successfully"

                };
            } catch (Exception ex)
            {
                return new ServiceResponse<Orders>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Order not placed try again later!"
                };
            }
        }

        public async Task<ServiceResponse<bool>> CancelOrderAsync(int orderId)
        {
            try
            {
                Orders order = await stopAndShopDBContext.Orders.FindAsync(orderId);

                if (order == null)
                {
                    return new ServiceResponse<bool>
                    {
                        Success = false,
                        ErrorMessage = "Order not found",
                        ResultMessage = "Try again!"
                    };

                }

                order.orderStatus = OrderStatus.CANCELLED;

                await stopAndShopDBContext.SaveChangesAsync();

                return new ServiceResponse<bool>
                {
                    Success = true,
                    Data = true,
                    ResultMessage = "Order successfully cancelled!"
                };

            } catch (Exception ex)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Error occured, please try again later!"
                };
            }
        }

        public async Task<ServiceResponse<List<Orders>>> FetchAllOrdersAsync(int userId)
        {
            try
            {
                if(!stopAndShopDBContext.Users.Any(u => u.userId == userId))
                {
                    return new ServiceResponse<List<Orders>>()
                    {
                        Data = null,
                        Success = false,
                        ErrorMessage = "User not found!",
                        ResultMessage = "Register user and try again"
                    };
                }


                List<Orders> orders = await stopAndShopDBContext.Orders.Where(i => i.userId == userId).ToListAsync();

                return new ServiceResponse<List<Orders>>()
                {
                    Data = orders,
                    Success = true,
                    ResultMessage = "Found all orders of the user"
                };

            }catch (Exception ex)
            {
                return new ServiceResponse<List<Orders>>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Error occured, try again later!"
                };
            }
        }

        public async Task<ServiceResponse<List<Orders>>> FetchAllOrdersSellerAsync(int sellerId)
        {
            try
            {
                if (!stopAndShopDBContext.Sellers.Any(s => s.sellerId == sellerId))
                {
                    return new ServiceResponse<List<Orders>>()
                    {
                        Success = false,
                        Data = null,
                        ErrorMessage = "Seller not found!",
                        ResultMessage = "Register seller and try again!"
                    };
                }

                var orders = await stopAndShopDBContext.Orders.Where(i => i.sellerId == sellerId).ToListAsync();

                return new ServiceResponse<List<Orders>>()
                {
                    Data = orders,
                    Success = true,
                    ResultMessage = orders.Count == 0 ? "No new orders found, please try after sometime!" : "Found all your orders!",
                };
            }catch (Exception ex)
            {
                return new ServiceResponse<List<Orders>>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Error occured please try again later!"
                };
            }
        }

        public async Task<ServiceResponse<bool>> ProcessNewOrdersAsync(int sellerId, int orderId)
        {
            try
            {
                if(!stopAndShopDBContext.Orders.Any(o => o.orderId == orderId))
                {
                    return new ServiceResponse<bool>()
                    {
                        Data = false,
                        Success = false,
                        ErrorMessage = "Order not found",
                        ResultMessage = "Please try again!"
                    };
                }

                Orders order = await stopAndShopDBContext.Orders.FirstOrDefaultAsync(i => i.sellerId == sellerId && i.orderId == orderId);
                order.orderStatus = OrderStatus.SHIPPED;
                await stopAndShopDBContext.SaveChangesAsync();

                return new ServiceResponse<bool>()
                {
                    Success = true,
                    ResultMessage = "Order has been shipped",
                    Data = true
                };

            }catch (Exception ex) 
            {
                return new ServiceResponse<bool>()
                {
                    Success = false,
                    ResultMessage = "There is some error try again later",
                    ErrorMessage = ex.Message
                };
            }
        }

        public async Task<ServiceResponse<bool>> CancelOrderSellerAsync(int sellerId, int orderId)
        {
            try
            {
                Orders order = await stopAndShopDBContext.Orders.FirstOrDefaultAsync(i => i.sellerId == sellerId && i.orderId == orderId);
                order.orderStatus = OrderStatus.CANCELLED;
                await stopAndShopDBContext.SaveChangesAsync();

                return new ServiceResponse<bool>()
                {
                    Success = true,
                    Data = true,
                    ResultMessage = "Your order has been cancelled by seller"
                };

            }catch (Exception ex)
            {
                return new ServiceResponse<bool>(){
                    Success = false,
                    ErrorMessage= ex.Message,
                    ResultMessage= "Not able to cancel, Try Again!"

                };
            }
        }

        public async Task<ServiceResponse<List<Orders>>> ShowNewOrdersPlaced(int sellerId, OrderStatus status)
        {
            try
            {
                if(!stopAndShopDBContext.Sellers.Any(s => s.sellerId == sellerId))
                {
                    return new ServiceResponse<List<Orders>>()
                    {
                        Success = false,
                        Data = null,
                        ResultMessage = "Please register seller!",
                        ErrorMessage = "Seller not registered"
                    };
                }

                List<Orders> orders = await stopAndShopDBContext.Orders.Where(i => i.sellerId == sellerId && i.orderStatus == status).ToListAsync();

                return new ServiceResponse<List<Orders>>()
                {
                    Success = true,
                    Data = orders,
                    ResultMessage = orders.Count == 0 ? "You have no new orders!" : "Found all your new orders"
                };

            }catch (Exception ex)
            {
                return new ServiceResponse<List<Orders>>()
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                    ResultMessage = "Some error occured, Try again later!"
                };
            }
        }
    }
}
