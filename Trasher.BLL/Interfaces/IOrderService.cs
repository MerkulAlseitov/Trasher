
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;
using Trasher.Domain.Enums;

namespace Trasher.BLL.Interfaces
{
    public interface IOrderService
    {
 public Task<IResponse<IEnumerable<OrderDTO>>> GetUnassignedRequests();


  public Task<IResponse<bool>> AssignRequestToTeam(int orderId, string brigadeId);

        public Task<IResponse<bool>> AssignRequestToOperator(int orderId, string operatorId);

        public Task<IResponse<bool>> CreateRequest(OrderDTO order);

        public Task<IResponse<bool>> UpdateRequest(OrderDTO order);

        public Task<IResponse<bool>> ChangeRequestStatus(int orderId, OrderStatus newStatus);
    }
}
