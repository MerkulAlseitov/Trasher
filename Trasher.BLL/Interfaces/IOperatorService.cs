using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;
using Trasher.Domain.Users;

namespace Trasher.BLL.Interfaces
{
    public interface IOperatorService
    {
        Task<IResponse<IEnumerable<OrderDTO>>> GetActiveOrders(string Id);

        Task<IResponse<IEnumerable<OrderDTO>>> GetClosedOrders(string Id);

        Task<IResponse<bool>> AcceptOrder(int orderId, string Id);

        Task<IResponse<bool>> MarkOrderAsCompleted(int orderId);

        Task<IResponse<IEnumerable<Operator>>> GetAllAsync();

        Task<IResponse<bool>> UpdateAsync(Operator user);

        Task<IResponse<bool>> CloseOrderByOperator(int orderId, string Id);
        Task<IResponse<Operator>> CreateOperator(OperatorDTO operatorDTO);
        Task<IResponse<Brigade>> CreateBrigade(BrigadeDTO bdrigademodel);
    }
}
