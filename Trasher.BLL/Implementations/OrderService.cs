using Microsoft.AspNetCore.Identity;
using Trasher.API.MODELS.Response;
using Trasher.BLL.Interfaces;
using Trasher.BLL.Mapping;
using Trasher.DAL.Repositories.Interfaces;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Enums;
using Trasher.Domain.Users;


namespace Trasher.BLL.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Order> _orderRepository;
        private readonly UserManager<Client> _userManager;
        public OrderService(IBaseRepository<Order> reviewRepository,
            UserManager<Client> userManager)
        {
            _orderRepository = reviewRepository;
            _userManager = userManager;
        }
        public async Task<IResponse<bool>> AssignOrderToOperator(int orderId, string operatorId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);

                order.OperatorId = operatorId;
                await _orderRepository.Update(order);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<bool>> AssignOrderToBrigade(int orderId, string brigadeId)
        {
            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);

                order.BrigadeId = brigadeId;
                await _orderRepository.Update(order);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<bool>> ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {

            try
            {
                var order = await _orderRepository.GetByIdAsync(orderId);

                order.OrderStatus = newStatus;
                await _orderRepository.Update(order);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<bool>> CreateOrder(OrderDTO order)
        {
            try
            {
                var newOrder = Mapper<OrderDTO, Order>.Map(order);

                var client = await _userManager.FindByIdAsync(newOrder.ClientId);
                newOrder.Client = client;
                newOrder.ClientId = client.Id;

                await _orderRepository.AddAsync(newOrder);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<IEnumerable<OrderDTO>>> GetUnassignedOrder()
        {
            try
            {
                var unassignedOrders = _orderRepository
                                 .GetAllAsync().Result
                                 .Where(order => order.OrderStatus == OrderStatus.Awaiting && order.OperatorId == null && order.BrigadeId == null);

                IEnumerable<OrderDTO> unassignedOrdersDTO = DTOMapper<Order, OrderDTO>.Map(unassignedOrders);

                return new Response<IEnumerable<OrderDTO>>(200, null, true, unassignedOrdersDTO);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderDTO>>(500, ex.Message, false, null);
            }
        }
        public async Task<IResponse<bool>> UpdateOrder(OrderDTO order)
        {
            try
            {
                var newRequest = DTOMapper<Order, OrderDTO>.Map(order);

                await _orderRepository.Update(newRequest);
                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }
        public async Task<IResponse<IEnumerable<Order>>> GetAll()
        {
            try
            {
                var entities = await _orderRepository.GetAllAsync();

                return new Response<IEnumerable<Order>>(200, null, true, entities);
            }

            catch (Exception ex)
            {
                return new Response<IEnumerable<Order>>(500, ex.Message, true, null);
            }
        }
    }
}