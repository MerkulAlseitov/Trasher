using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.API.MODELS.Response;
using Trasher.BLL.Interfaces;
using Trasher.DAL.Repositories.Interfaces;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;

namespace Trasher.BLL.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IBaseRepository<Review> _reviewRepository;

        public ReviewService(IBaseRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async Task<IResponse<IEnumerable<ReviewDTO>>> GetReviewsByRequestId(int OrderId)
        {
            try
            {
                // Ваша логика для получения отзывов по идентификатору запроса
                var reviews = await _reviewRepository.GetAllAsync().Result.Where(review => review.Order.Id == OrderId).ToList();
                return new Response<IEnumerable<ReviewDTO>>(200, null, true, reviews);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ReviewDTO>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<bool>> CreateReview(ReviewDTO review)
        {
            try
            {
                // Ваша логика для создания нового отзыва
                await _reviewRepository.Create(review);
                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<bool>> UpdateReview(ReviewDTO review)
        {
            try
            {
                // Ваша логика для обновления существующего отзыва
                await _reviewRepository.Update(review);
                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }
    }
}
