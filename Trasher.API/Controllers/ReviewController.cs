using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trasher.API.MODELS.Response;
using Trasher.BLL.Implementations;
using Trasher.BLL.Interfaces;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;

namespace Trasher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet("byOrderId/{orderId}")]
        public IActionResult GetReviewsByOrderId(int orderId)
        {
            var response = _reviewService.GetReviewsByOrderId(orderId);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewDTO review)
        {
            var response = await _reviewService.CreateReview(review);

            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateReview(ReviewDTO review)
        {
            var response = await _reviewService.UpdateReview(review);

            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }
    }
}
