using LibraryModule.Models;

namespace LibraryModule.Service
{
    public interface IBookLibraryService
    {
        void AddBook(string title, string author);
        bool BorrowBook(int userId, int bookId);
        List<Book> GetBorrowedBooks(int userId);
        void RegisterUser(string name);
        bool RemoveBook(int bookId);
        bool ReturnBook(int userId, int bookId);
        List<Book> SearchBooks(string query);

        public List<Book> GetAllBooks();
    }
}