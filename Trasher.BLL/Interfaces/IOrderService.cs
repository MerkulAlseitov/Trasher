
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Interfaces
{
    public interface IOrderService
    {
        public Task<IResponse<IEnumerable<OrderDTO>>> GetReviewsByRequestId(int requestId);

        public Task<IResponse<bool>> CreateReview(OrderDTO order);

        public Task<IResponse<bool>> UpdateReview(OrderDTO order);
    }
}
