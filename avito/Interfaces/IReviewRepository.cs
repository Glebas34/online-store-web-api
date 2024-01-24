using avito.Models;

namespace avito.Interfaces
{
    public interface IReviewRepository
    {
        Task<List<Review>> GetReviews();
        Task<Review> GetReview(int id);
        bool DeleteReview(Review review);
        bool CreateReview(Review review);
        bool UpdateReview(Review review);
        bool Save();
        bool ReviewExists(int id);
    }
}
