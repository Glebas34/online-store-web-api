using System.ComponentModel.DataAnnotations.Schema;

namespace avito.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        [ForeignKey(nameof(Seller))]
        public string SellerId { get; set; }
        public AppUser Seller { get; set; }
    }
}
