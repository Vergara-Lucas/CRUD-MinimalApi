namespace MinimalApisCrud.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
    }
    public record BookRequest (string Name, string Code );
}
