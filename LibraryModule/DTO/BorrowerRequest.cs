namespace LibraryModule.DTO
{
    // DTO for borrow and return requests
    public class BorrowRequest
    {
        public int UserId { get; set; }
        public int BookId { get; set; }
    }
}
