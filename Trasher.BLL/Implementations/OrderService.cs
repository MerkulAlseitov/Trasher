using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.Mapping;
using Trasher.API.MODELS.Response;
using Trasher.BLL.Interfaces;
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
        public async Task<IResponse<bool>> AssignRequestToOperator(int orderId, string operatorId)
        {
            try
            {
                var closedOrder = _orderRepository
               .GetAllAsync().Result
               .Where(r => r.OperatorId == operatorId && r.OrderStatus == OrderStatus.Compleated)
               .ToList();

                IEnumerable<OrderDTO> closedOrderDTO = Mapper<Order, OrderDTO>.Map(closedOrder);
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

        public async Task<IResponse<bool>> ChangeRequestStatus(int orderId, OrderStatus newStatus)
        {

            try
            {
                var request = await _orderRepository.GetByIdAsync(orderId);

                request.OrderStatus = newStatus;
                await _orderRepository.Update(request);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<bool>> CreateRequest(OrderDTO order)
        {
            try
            {
                var newRequest = DTOMapper<OrderDTO, Order>.Map(order);

                var client = await _userManager.FindByIdAsync(newRequest.ClientId);
                newRequest.Client = client;

                await _orderRepository.Update(newRequest);
                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<IEnumerable<OrderDTO>>> GetUnassignedRequests()
        {
            try
            {
                var unassignedRequests = _orderRepository
                                 .GetAllAsync().Result
                                 .Where(request => request.OrderStatus == OrderStatus.InProgress && request.OperatorId == null && request.BrigadeId == null);

                IEnumerable<OrderDTO> unassignedRequestsDTO = Mapper<Order, OrderDTO>.Map(unassignedRequests);

                return new Response<IEnumerable<OrderDTO>>(200, null, true, unassignedRequestsDTO);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<OrderDTO>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<bool>> UpdateRequest(OrderDTO order)
        {
            try
            {
                var newRequest = DTOMapper<OrderDTO, Order>.Map(order);

                await _orderRepository.Update(newRequest);
                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }
    }
}
