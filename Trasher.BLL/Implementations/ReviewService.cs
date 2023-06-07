using Trasher.API.MODELS.Response;
using Trasher.BLL.Interfaces;
using Trasher.BLL.Mapping;
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

        public IResponse<IEnumerable<ReviewDTO>> GetReviewsByOrderId(int OrderId)
        {
            try
            {
                var reviews = _reviewRepository.GetAllAsync().Result.Where(review => review.Order.Id == OrderId).ToList();
                var reviewsDTO = DTOMapper<Review, ReviewDTO>.Map(reviews);
                return new Response<IEnumerable<ReviewDTO>>(200, null, true, reviewsDTO);
            }
            catch (Exception ex)
            {
                return new Response<IEnumerable<ReviewDTO>>(500, ex.Message, false, null);
            }
        }

        public async Task<IResponse<bool>> CreateReview(ReviewDTO reviewDTO)
        {
            try
            {
                if (reviewDTO == null)
                {
                    throw new ArgumentNullException("Review is null.");
                }

                var newReview = Mapper<ReviewDTO, Review>.Map(reviewDTO);
                await _reviewRepository.AddAsync(newReview);
                await _reviewRepository.Update(newReview);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }

        public async Task<IResponse<bool>> UpdateReview(ReviewDTO reviewDTO)
        {
            try
            {
                if (reviewDTO == null)
                {
                    throw new ArgumentNullException("Review is null.");
                }

                var newReview = DTOMapper<Review, ReviewDTO>.Map(reviewDTO);
                await _reviewRepository.Update(newReview);

                return new Response<bool>(200, null, true, true);
            }
            catch (Exception ex)
            {
                return new Response<bool>(500, ex.Message, false, false);
            }
        }
    }
}