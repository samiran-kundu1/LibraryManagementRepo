using LibraryModule.Models;
using System.Collections.Generic;
using System.Xml.Linq;

namespace LibraryModule.Service
{
    public class BookLibraryService : IBookLibraryService
    {
        private List<Book> books = new List<Book>();
        private List<User> users = new List<User>();

        public BookLibraryService()
        {
            InitializeSampleData();
        }

        private void InitializeSampleData()
        {
            books.AddRange(new List<Book>
        {
            new Book { Id = 1,Title = "Test Book 1", Author ="Test Author 1" ,IsAvailable = false },
            new Book { Id = 2,Title = "Test Book 2", Author ="Test Author 2" ,IsAvailable = false },
            new Book { Id = 3,Title = "Test Book 3", Author ="Test Author 3" ,IsAvailable = true },
            new Book { Id = 4,Title = "Test Book 4", Author ="Test Author 4" ,IsAvailable = false },
            new Book { Id = 5,Title = "Test Book 5", Author ="Test Author 5" ,IsAvailable = true }
        });
            
            users.AddRange(new List<User>
            {
            new User { Id = 1,Name = "UserName 1", BorrowedBookIds = new List<int>() { 1,4 } },
            new User { Id = 2,Name = "UserName 2", BorrowedBookIds = new List<int>() { 2}},
        
            
        });
        }

        // Add a new book to the library
        public void AddBook(string title, string author)
        {
            //Chekc if book specified is already available for borrowing
            var book = books.Where(b=>b.Title == title && b.Author == author && b.IsAvailable).FirstOrDefault();
            if (book!=null)
            {
                throw new InvalidOperationException($"A book with title {title}  and author {author} already exists" +
                    $"and is available to borrow.");
            }
            books.Add(new Book { Title = title, Author = author , Id = books.Count + 1 , IsAvailable = true });
        }

        // Remove a book from the library by ID
        public bool RemoveBook(int bookId)
        {
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (book != null && book.IsAvailable)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        // Search for books by title or author
        public List<Book> SearchBooks(string query)
        {
            return books.Where(b =>
                b.Title.Contains(query, StringComparison.OrdinalIgnoreCase) ||
                b.Author.Contains(query, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        // Register a new user
        public void RegisterUser(string name)
        {
            users.Add(new User { Name = name });
        }

        // Borrow a book
        public bool BorrowBook(int userId, int bookId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (user != null && book != null && book.IsAvailable)
            {
                book.IsAvailable = false;
                user.BorrowedBookIds.Add(book.Id);
                return true;
            }
            return false;
        }

        // Return a borrowed book
        public bool ReturnBook(int userId, int bookId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            var book = books.FirstOrDefault(b => b.Id == bookId);
            if (user != null && book != null && !book.IsAvailable && user.BorrowedBookIds.Contains(book.Id))
            {
                book.IsAvailable = true;
                user.BorrowedBookIds.Remove(book.Id);
                return true;
            }
            return false;
        }

        // List all books currently borrowed by a specific user
        public List<Book> GetBorrowedBooks(int userId)
        {
            var user = users.FirstOrDefault(u => u.Id == userId);
            List < Book > borrowedBooks = new List<Book>();
            if (user != null)
            {

                foreach (var borrowedBookId in user.BorrowedBookIds)
                {
                    borrowedBooks.Add(books.Where(b => b.Id == borrowedBookId).FirstOrDefault());
                }
                
            }
            return borrowedBooks;
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }
    }

}
