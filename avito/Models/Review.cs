using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace avito.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [Range(1,5)]
        public int Rating { get; set; }
        [ForeignKey(nameof(Reviewer))]
        public string ReviewerId { get; set; }
        public AppUser Reviewer { get; set; }
        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
