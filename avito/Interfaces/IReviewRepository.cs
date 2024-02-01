using avito.Models;

namespace avito.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviews();
        Task<Review> GetReview(int id);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool DeleteReview(int id);
        bool Save();
        bool DeleteReviews(List<Review> reviews);
        bool ReviewExists(int id);
    }
}
