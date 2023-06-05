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
    public class OperatorService : IOperatorService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly UserManager<Operator> _userManager;

        public OperatorService(IBaseRepository<Order> orderRepository, UserManager<Operator> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }


        public async Task<IResponse<bool>> CreateOperator(OperatorDTO operatorDTO)
        {
            try
            {
                var existingOperator = await _userManager.FindByNameAsync(operatorDTO.FirstName);

                if (existingOperator != null)
                    return new Response<bool>(400, "Username already exists", false, false);

                var operatorUser = new Operator
                {
                    UserName = operatorDTO.UserName,
                    Email = operatorDTO.Email,
                    FirstName = operatorDTO.FirstName
                };

                var result = await _userManager.CreateAsync(operatorUser, operatorDTO.Password);

                if (result.Succeeded)
                {
                    return new Response<bool>(200, null, true, true);
                }
                else
                {
                    var errors = result.Errors.Select(e => e.Description).ToList();
                    return new Response<bool>(400, $"Operator creation failed, Errors: {errors}", false, false);
                }
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }


        public async Task<IResponse<IEnumerable<OrderDTO>>> GetActiveOrders(string Id)
        {
            try
            {
                var activeOrders = _orderRepository.GetAllAsync().Result
                    .Where(o => o.OperatorId == Id && o.OrderStatus == OrderStatus.InProgress);

                var activeOrdersDTO = Mapper<Order, OrderDTO>.Map(activeOrders);

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
                    .Where(o => o.OperatorId == Id && o.OrderStatus == OrderStatus.Completed);

                var closedOrdersDTO = Mapper<Order, OrderDTO>.Map(closedOrders);

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

                order.OperatorId = Id;
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

        public async Task<IResponse<IEnumerable<Operator>>> GetAllAsync()
        {
            try
            {
                var operators = await _userManager.GetUsersInRoleAsync("Operator");

                return new Response<IEnumerable<Operator>>(200, null, true, operators);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Operator>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<bool>> UpdateAsync(Operator user)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);

                if (existingUser == null)
                    return new Response<bool>(404, "Operator not found", false, false);

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

        public async Task<IResponse<bool>> CloseOrderByOperator(int orderId, string Id)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);

                if (order == null)
                    return new Response<bool>(404, "Order not found", false, false);

                if (order.OperatorId != Id)
                    return new Response<bool>(403, "You are not authorized to close this order", false, false);

                order.OrderStatus = OrderStatus.Completed;

                await _orderRepository.Update(order);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }
    }
}