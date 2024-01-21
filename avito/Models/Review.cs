namespace avito.Models
{
    public class Review
    {
        public int Id { get; set; }
        public string Text {  get; set; }
        public int Rating { get; set; }
        public AppUser Seller { get; set; }
        public AppUser Reviewer { get; set; }
    }
}
