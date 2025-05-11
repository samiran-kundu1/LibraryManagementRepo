namespace LibraryModule.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> BorrowedBookIds { get; set; } = new List<int>();
    }

}
