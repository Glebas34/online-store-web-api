using System.ComponentModel.DataAnnotations;

namespace avito.Dto
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        [Range(1, 5)]
        public int Rating { get; set; }
    }
}
