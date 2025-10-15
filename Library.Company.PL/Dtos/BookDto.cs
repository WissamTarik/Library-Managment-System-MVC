using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Library.Company.PL.Dtos
{
    public class BookDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "ISBN must be greater than zero")]
        public int ISBN { get; set; }
        public string Title { get; set; }
        [Range(1500, 2025, ErrorMessage = "Publish year must be between 1500 and 2025")]
        [DisplayName("Published Year")]
        public int PublishedYear { get; set; }
        public string? AuthorName { get; set; }
        public int? AuthorId { get; set; }

        public string? CategoryName { get; set; }
        public int? CategoryId { get; set; }

    }
}
