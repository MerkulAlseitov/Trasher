using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;
using Trasher.Domain.Users;

namespace Trasher.BLL.IClientService
{
    public interface IClientService
    {
        Task<IResponse<IEnumerable<OrderDTO>>> GetActiveOrders(string Id);

        Task<IResponse<IEnumerable<OrderDTO>>> GetClosedOrders(string Id);

        Task<IResponse<bool>> AcceptOrder(int orderId, string Id);

        Task<IResponse<bool>> MarkOrderAsCompleted(int orderId);

        Task<IResponse<IEnumerable<Client>>> GetAllAsync();

        Task<IResponse<bool>> UpdateAsync(Client user);
        Task<IResponse<bool>> CreateClient(ClientDTO client);
    }
}
