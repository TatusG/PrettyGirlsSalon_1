using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;
using AccesoDatosSalon.Opetarions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private ReviewDAO reviewDAO = new ReviewDAO();

        [HttpGet("reviews")]
        public List<Review> GetAllReviews()
        {
            return reviewDAO.allReviews();
        }

        [HttpGet("review")]
        public Review GetReview(int id)
        {
            return reviewDAO.getReview(id);
        }

        [HttpPost("reviews")]
        public bool CreateReview([FromBody] ReviewCreateDTO reviewDTO)
        {
            if (reviewDTO == null || reviewDTO.RatingValue < 1 || reviewDTO.RatingValue > 5 || reviewDTO.AppintmentId <= 0)
            {
                return false;
            }
            else
            {
                return reviewDAO.addReview(
                    reviewDTO.AppintmentId,
                    reviewDTO.RatingValue,
                    reviewDTO.ReviewComment,
                    DateOnly.FromDateTime(DateTime.Now),
                    null
                );
            }
        }

        [HttpPut("review")]
        public bool UpdateReview([FromBody] ReviewUpdateDTO reviewDTO)
        {
            if (reviewDAO == null || reviewDTO.id <= 0 || string.IsNullOrWhiteSpace(reviewDTO.response))
            {
                return false;
            }
            else
            {
                var existingReview = reviewDAO.getReview(reviewDTO.id);
                if (existingReview == null)
                {
                    return false; // Review not found
                }
                else
                {
                    return reviewDAO.updateReview(
                    reviewDTO.id,
                    existingReview.AppointmentId,
                    existingReview.RatingValue,
                    existingReview.ReviewComment,
                    existingReview.ReviewDate,
                    reviewDTO.response
                );
                }
            }
        }

        [HttpDelete("review")]
        public bool DeleteReview(int id)
        {
            return reviewDAO.deleteReview(id);
        }

        [HttpGet("reviews/appointment")]

        public Review GetReviewByAppointment(int appointmentId)
        {
            var reviews = reviewDAO.allReviews().Where(r => r.AppointmentId == appointmentId).ToList();
            return reviews.FirstOrDefault();
        }

        [HttpGet("reviews/stylist")]
        public List<Review> GetReviewsByStylist(string stylistUser)
        {
            var reviews = reviewDAO.allReviews().Where(r => r.Appointment.StylistUser == stylistUser).ToList();
            return reviews;
        }
    }
}
