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
        private readonly IReviewService _ireviewService;
        public ReviewController(IReviewService reviewService)
        {
            _ireviewService = reviewService;
        }

        [HttpGet("GetReviewsByOrderId")]
        public IActionResult GetReviewsByOrderId(int orderId)
        {
            var response = _ireviewService.GetReviewsByOrderId(orderId);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }

        [HttpPost]
        [Route("CreateReview")]
        public async Task<IActionResult> CreateReview(ReviewDTO review)
        {
            var response = await _ireviewService.CreateReview(review);

            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }
        [HttpPut]
        [Route("UpdateReview")]
        public async Task<IActionResult> UpdateReview(ReviewDTO review)
        {
            var response = await _ireviewService.UpdateReview(review);

            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }
    }
}
