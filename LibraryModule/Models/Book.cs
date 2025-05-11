using Swashbuckle.AspNetCore.Annotations;

namespace LibraryModule.Models
{
    public class Book
    {
        [SwaggerSchema(ReadOnly = true)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

}