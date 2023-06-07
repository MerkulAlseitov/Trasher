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
        private readonly UserManager<Brigade> _userManagerBrigade;

        public OperatorService(IBaseRepository<Order> orderRepository, 
            UserManager<Operator> userManager, 
            UserManager<Brigade> userManagerBrigade)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
            _userManagerBrigade = userManagerBrigade;
        }

        public async Task<IResponse<Brigade>> CreateBrigade(BrigadeDTO bdrigademodel)
        {
            try
            {
                var userExists = await _userManagerBrigade.FindByNameAsync(bdrigademodel.UserName);
                if (userExists != null)
                {
                    throw new UnauthorizedAccessException(" Thsit User already exists! Please check user Name");
                }

                var user = new Brigade
                {
                    Email = bdrigademodel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = bdrigademodel.UserName
                };

                var result = await _userManagerBrigade.CreateAsync(user, bdrigademodel.Password);
                string userId = user.Id;
                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException("User creation failed! Please check user details and try again." +
                        $"  Identity Errors: Enter correct password");
                }
                return new Response<Brigade>(200, null, true, user);
            }
            catch (Exception ex)
            {
                return new Response<Brigade>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<Operator>> CreateOperator(OperatorDTO operatormodel)
        {
            try
            {

                var userExists = await _userManager.FindByNameAsync(operatormodel.UserName);
                if (userExists != null)
                {
                    throw new UnauthorizedAccessException(" Thsit User already exists! Please check user Name");
                }

                var user = new Operator
                {
                    Email = operatormodel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = operatormodel.UserName
                };

                var result = await _userManager.CreateAsync(user, operatormodel.Password);
                string userId = user.Id;
                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException("User creation failed! Please check user details and try again." +
                        $"  Identity Errors: Enter correct password");
                }
                return new Response<Operator>(200, null, true, user);
            }
            catch (Exception ex)
            {
                return new Response<Operator>(500, ex.Message, false, null);
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