using LibraryModule.DTO;
using LibraryModule.Models;

namespace LibraryModule.Mapper
{
    public static partial class MapperExtension
    {
        public static List<BookDTO> MapToBooksDTOList(this List<Book> books)
        {
            return books.Select(book =>  book.MapToBookDTO()).ToList();
        }
        public static BookDTO MapToBookDTO(this Book req)
        {
            return new BookDTO
            {
                Author = req.Author,
                Title = req.Title
            };
        }
    }
}
