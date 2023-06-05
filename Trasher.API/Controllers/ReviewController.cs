using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trasher.API.MODELS.Response;
using Trasher.Domain.DTOs;

namespace Trasher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        //public interface IReviewService
        //{
        //    public IResponse<IEnumerable<ReviewDTO>> GetReviewsByOrderId(int OrderId);

        //    public Task<IResponse<bool>> CreateReview(ReviewDTO review);

        //    public Task<IResponse<bool>> UpdateReview(ReviewDTO review);
        //}
    }
}
