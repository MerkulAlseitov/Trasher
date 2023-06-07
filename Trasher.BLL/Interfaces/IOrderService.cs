using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Enums;

namespace Trasher.BLL.Interfaces
{
    public interface IOrderService
    {
        public Task<IResponse<IEnumerable<OrderDTO>>> GetUnassignedOrder();
        public Task<IResponse<bool>> AssignOrderToBrigade(int orderId, string brigadeId);
        public Task<IResponse<bool>> AssignOrderToOperator(int orderId, string operatorId);
        public Task<IResponse<bool>> CreateOrder(OrderDTO order);
        public Task<IResponse<bool>> UpdateOrder(OrderDTO order);
        public Task<IResponse<bool>> ChangeOrderStatus(int orderId, OrderStatus newStatus);
        public Task<IResponse<IEnumerable<Order>>> GetAll();
    }
}
