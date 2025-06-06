using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AccesoDatosSalon.Opetarions
{
    public class ReviewDAO
    {
        public PrettyGirlSalonContext contexto = new PrettyGirlSalonContext();

        public List<Review> allReviews()
        {
            var reviews = contexto.Reviews.ToList<Review>();
            return reviews;
        }

        public Review getReview(int id)
        {
            var review = contexto.Reviews.Where(v => v.Id == id).FirstOrDefault();
            return review;
        }

        public bool addReview(int appointmentId, int rating, string commen, DateOnly date, string response)
        {
            try
            {
                Review review = new Review();

                review.AppointmentId = appointmentId;
                review.RatingValue = rating;
                review.ReviewComment = commen;
                review.ReviewDate = date;
                review.Response= response;
                
                contexto.Reviews.Add(review);
                contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateReview(int id, int appointmentId, int rating, string commen, DateOnly date, string response) 
        {
            try
            {
                var review = getReview(id);
                if (review == null)
                {
                    return false;
                }
                else
                {
                    review.AppointmentId = appointmentId;
                    review.RatingValue = rating;
                    review.ReviewComment = commen;
                    review.ReviewDate = date;
                    review.Response = response;
                    contexto.SaveChanges();
                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool deleteReview(int id)
        {
            try
            {
                var review = getReview(id);
                if (review == null)
                {
                    return false;
                }
                else
                {
                    contexto.Reviews.Remove(review);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
