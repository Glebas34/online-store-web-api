using avito.Models;

namespace avito.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviews();
        Task<Review> GetReview(int id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(Review review);
        bool Save();
        bool DeleteReviews(List<Review> reviews);
        bool ReviewExists(int id);
    }
}
