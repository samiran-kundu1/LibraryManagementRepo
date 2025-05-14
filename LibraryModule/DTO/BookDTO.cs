using System.ComponentModel.DataAnnotations;

namespace LibraryModule.DTO
{
    public class BookDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Author { get; set; }
    }
}
