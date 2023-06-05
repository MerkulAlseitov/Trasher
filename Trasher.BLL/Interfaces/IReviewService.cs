using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;

namespace Trasher.BLL.Interfaces
{
    public interface IReviewService
    {
        public IResponse<IEnumerable<ReviewDTO>> GetReviewsByOrderId(int OrderId);

        public Task<IResponse<bool>> CreateReview(ReviewDTO review);

        public Task<IResponse<bool>> UpdateReview(ReviewDTO review);
    }
}
