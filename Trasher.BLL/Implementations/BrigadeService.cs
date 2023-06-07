using Microsoft.AspNetCore.Identity;
using Trasher.BLL.Mapping;
using Trasher.API.MODELS.Response;
using Trasher.BLL.Interfaces;
using Trasher.DAL.Repositories.Interfaces;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Enums;
using Trasher.Domain.Users;

namespace Trasher.BLL.Implementations
{
    public class BrigadeService : IBrigadeService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly UserManager<Brigade> _userManager;

        public BrigadeService(IBaseRepository<Order> orderRepository, UserManager<Brigade> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }


        public async Task<IResponse<IEnumerable<OrderDTO>>> GetActiveOrders(string Id)
        {
            try
            {
                var activeOrders = _orderRepository.GetAllAsync().Result
                    .Where(o => o.BrigadeId == Id && o.OrderStatus == OrderStatus.InProgress);

                var activeOrdersDTO = DTOMapper<Order, OrderDTO>.Map(activeOrders);

                return new Response<IEnumerable<OrderDTO>>(200, null, true, activeOrdersDTO);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderDTO>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<IEnumerable<OrderDTO>>> GetClosedOrders(string Id)
        {
            try
            {
                var closedOrders = _orderRepository.GetAllAsync().Result
                    .Where(o => o.BrigadeId == Id && o.OrderStatus == OrderStatus.Completed);

                var closedOrdersDTO = DTOMapper<Order, OrderDTO>.Map(closedOrders);

                return new Response<IEnumerable<OrderDTO>>(200, null, true, closedOrdersDTO);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderDTO>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<bool>> AcceptOrder(int orderId, string Id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);

                if (order == null)
                    return new Response<bool>(404, "Order not found", false, false);

                if (order.OrderStatus != OrderStatus.InProgress)
                    return new Response<bool>(400, "Invalid order status", false, false);

                order.BrigadeId = Id;
                order.OrderStatus = OrderStatus.Accepted;

                await _orderRepository.Update(order);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<bool>> MarkOrderAsCompleted(int orderId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);

                if (order == null)
                    return new Response<bool>(404, "Order not found", false, false);

                if (order.OrderStatus != OrderStatus.Accepted)
                    return new Response<bool>(400, "Invalid order status", false, false);

                order.OrderStatus = OrderStatus.Completed;

                await _orderRepository.Update(order);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<IEnumerable<Brigade>>> GetAllAsync()
        {
            try
            {
                var brigades = _userManager.Users.ToList();
                return new Response<IEnumerable<Brigade>>(200, null, true, brigades);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Brigade>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<bool>> UpdateAsync(Brigade user)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);

                if (existingUser == null)
                    return new Response<bool>(404, "Brigade not found", false, false);

                existingUser.FirstName = user.FirstName;
                existingUser.Email = user.Email;

                await _userManager.UpdateAsync(existingUser);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }
    }
}