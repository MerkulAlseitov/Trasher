using Microsoft.EntityFrameworkCore.Storage.Internal;
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

namespace Trasher.BLL.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IBaseRepository<Review> _reviewRepository;

        public ReviewService(IBaseRepository<Review> reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public  IResponse<IEnumerable<ReviewDTO>> GetReviewsByOrderId(int OrderId)
        {
            try
            {
                var reviews =  _reviewRepository.GetAllAsync().Result.Where(review => review.Order.Id == OrderId).ToList();
                var reviewsDTO = Mapper<Review, ReviewDTO>.Map(reviews);
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

                var newReview = DTOMapper<ReviewDTO, Review>.Map(reviewDTO);
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

                var newReview = DTOMapper<ReviewDTO, Review>.Map(reviewDTO);
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
