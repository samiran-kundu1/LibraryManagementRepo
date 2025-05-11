using System;
using System.Linq;
using Xunit;
using LibraryModule.Service;
using LibraryModule.Models;
using System.Collections.Generic;

namespace LibraryModule.Tests
{
    public class BookLibraryServiceTests
    {
        private readonly BookLibraryService _service;

        public BookLibraryServiceTests()
        {
            _service = new BookLibraryService();
        }

        [Fact]
        public void AddBook_Should_Increase_Book_Count()
        {
            // Arrange
            var initialCount = _service.GetAllBooks().Count;

            // Act
            _service.AddBook("New Book", "New Author");

            // Assert
            var books = _service.GetAllBooks();
            Assert.Equal(initialCount + 1, books.Count);
            Assert.Contains(books, b => b.Title == "New Book" && b.Author == "New Author");
        }

        [Fact]
        public void RemoveBook_Should_Remove_Available_Book()
        {
            // Arrange
            var book = _service.GetAllBooks().FirstOrDefault(b => b.IsAvailable);
            Assert.NotNull(book);

            // Act
            var result = _service.RemoveBook(book.Id);

            // Assert
            Assert.True(result);
            Assert.DoesNotContain(_service.GetAllBooks(), b => b.Id == book.Id);
        }

        [Fact]
        public void RemoveBook_Should_Fail_If_Book_Is_Borrowed()
        {
            // Arrange
            var user = _service.GetBorrowedBooks(1).Any() ? _service.GetBorrowedBooks(1).First() : null;
            if (user == null)
            {
                _service.BorrowBook(1, 2);
                user = _service.GetAllBooks().First(b => b.Id == 2);
            }

            // Act
            var result = _service.RemoveBook(user.Id);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public void SearchBooks_Should_Return_Correct_Results()
        {
            // Act
            var results = _service.SearchBooks("Test Book 1");

            // Assert
            Assert.Single(results);
            Assert.Equal("Test Book 1", results[0].Title);
        }

        [Fact]
        public void RegisterUser_Should_Add_New_User()
        {
            // Act
            _service.RegisterUser("New User");

            // Assert
            var userExists = _service.GetBorrowedBooks(3) != null;
            Assert.True(userExists);
        }

        [Fact]
        public void BorrowBook_Should_Mark_Book_As_Unavailable()
        {
            // Arrange
            var book = _service.GetAllBooks().FirstOrDefault(b => b.IsAvailable);
            Assert.NotNull(book);

            // Act
            var result = _service.BorrowBook(1, book.Id);

            // Assert
            Assert.True(result);
            Assert.False(book.IsAvailable);
        }

        [Fact]
        public void ReturnBook_Should_Mark_Book_As_Available()
        {
            // Arrange
            var book = _service.GetAllBooks().FirstOrDefault(b => b.IsAvailable);
            Assert.NotNull(book);
            _service.BorrowBook(1, book.Id);

            // Act
            var result = _service.ReturnBook(1, book.Id);

            // Assert
            Assert.True(result);
            Assert.True(book.IsAvailable);
        }

        [Fact]
        public void GetBorrowedBooks_Should_Return_Correct_Books()
        {
            // Arrange
            var userId = 1;

            // Act
            var borrowedBooks = _service.GetBorrowedBooks(userId);

            // Assert
            Assert.NotNull(borrowedBooks);
            Assert.All(borrowedBooks, b => Assert.Contains(b.Id, new List<int> { 1,4 }));
        }
    }
}
