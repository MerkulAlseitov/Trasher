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

namespace Trasher.BLL.Implementations
{

    public class OrderService : IOrderService
    {
        private readonly IBaseRepository<Review> _reviewRepository;
        public OrderService(IBaseRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public Task<IResponse<bool>> AssignRequestToOperator(int orderId, string operatorId)
        {
            var closedRequests = await _repository
                    .ReadAllAsync().Result
                    .Where(r => r.OperatorId == operatorId && r.RequestStatus == Status.Closed)
                    .ToListAsync();

            IEnumerable<RequestDTO> closedRequestsDTO = MapperHelperForDto<Request, RequestDTO>.Map(closedRequests);
            throw new NotImplementedException();
        }

        public Task<IResponse<bool>> AssignRequestToTeam(int orderId, string brigadeId)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<bool>> ChangeRequestStatus(int orderId, OrderStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<bool>> CreateRequest(OrderDTO order)
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<IEnumerable<OrderDTO>>> GetUnassignedRequests()
        {
            throw new NotImplementedException();
        }

        public Task<IResponse<bool>> UpdateRequest(OrderDTO order)
        {
            throw new NotImplementedException();
        }


    }
}
