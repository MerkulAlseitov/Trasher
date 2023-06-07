using Microsoft.AspNetCore.Identity;
using Trasher.BLL.Mapping;
using Trasher.API.MODELS.Response;
using Trasher.DAL.Repositories.Interfaces;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Enums;
using Trasher.Domain.Users;
using Trasher.BLL.Interfaces;

namespace Trasher.BLL.Implementations
{
    public class ClientService : IClientService

    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly UserManager<Client> _userManager;

        public ClientService(IBaseRepository<Order> orderRepository, UserManager<Client> userManager)
        {
            _orderRepository = orderRepository;
            _userManager = userManager;
        }

        public async Task<IResponse<Client>> CreateClient(ClientDTO clientModel)
        {
            try
            {

                var userExists = await _userManager.FindByNameAsync(clientModel.UserName);
                if (userExists != null)
                {
                    throw new UnauthorizedAccessException(" Thsit User already exists! Please check user Name");
                }

                var user = new Client
                {
                    Email = clientModel.Email,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = clientModel.UserName
                };

                var result = await _userManager.CreateAsync(user, clientModel.Password);
                string userId = user.Id;
                if (!result.Succeeded)
                {
                    throw new UnauthorizedAccessException("User creation failed! Please check user details and try again." +
                        $"  Identity Errors: Enter correct password");
                }
                return new Response<Client>(200, null, true, user);
            }
            catch (Exception ex)
            {
                return new Response<Client>(500, ex.Message, false, null);
            }
        }
        public async Task<IResponse<IEnumerable<OrderDTO>>> GetActiveOrders(string Id)
        {
            try
            {
                var activeOrders = _orderRepository.GetAllAsync().Result
                    .Where(o => o.ClientId == Id && o.OrderStatus == OrderStatus.InProgress);

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
                    .Where(o => o.ClientId == Id && o.OrderStatus == OrderStatus.Completed);

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

                if (order.OrderStatus != OrderStatus.Accepted)
                    return new Response<bool>(400, "Invalid order status", false, false);

                order.ClientId = Id;
                order.OrderStatus = OrderStatus.InProgress;

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

                if (order.OrderStatus != OrderStatus.InProgress)
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

        public async Task<IResponse<IEnumerable<Client>>> GetAllAsync()
        {
            try
            {
                var clients = _userManager.Users.ToList();

                return new Response<IEnumerable<Client>>(200, null, true, clients);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<Client>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<bool>> UpdateAsync(Client user)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(user.Id);

                if (existingUser == null)
                    return new Response<bool>(404, "Client not found", false, false);

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