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
        private readonly IBaseRepository<Order> _orderRepository;   

        public ReviewService(IBaseRepository<Review> reviewRepository, IBaseRepository<Order> orderRepository)
        {
            _reviewRepository = reviewRepository;
            _orderRepository = orderRepository;
        }

        public IResponse<IEnumerable<ReviewDTO>> GetReviewsByOrderId(int OrderId)
        {
            try
            {
                var reviews = _reviewRepository.GetAllAsync().Result.Where( r => r.Id == OrderId).ToList();
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
                var newReview = Mapper<ReviewDTO, Review>.Map(reviewDTO);

                var order = _orderRepository.GetAllAsync().Result
                    .FirstOrDefault(order => order.Id == reviewDTO.orderId);

                //if (request == null  request.OrderStatus != OrderStatus.Completed  request.Review != null)
                //    throw new InvalidOperationException("Unable to create a review for the specified request.");


                order.ReviewId = newReview.Id;
                order.Review = newReview;

                await _reviewRepository.AddAsync(newReview);

                await _orderRepository.Update(order);

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

                var newReview = Mapper<ReviewDTO, Review>.Map(reviewDTO);
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