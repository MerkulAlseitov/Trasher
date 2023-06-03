using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;

namespace Trasher.BLL.Implementations
{
    public interface IReviewService
    {
        public Task<IResponse<IEnumerable<ReviewDTO>>> GetReviewsByRequestId(int requestId);

        public Task<IResponse<bool>> CreateReview(ReviewDTO review);

        public Task<IResponse<bool>> UpdateReview(ReviewDTO review);
    }
}
