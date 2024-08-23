using MinimalApisCrud.Context;
using MinimalApisCrud.Models;

namespace MinimalApisCrud.Services.Interfaces
{
    public class BookServices : IBookService
    {
        private readonly ApiContext _context;
        public BookServices(ApiContext context)
        {
            _context = context;
        }
        async Task<Book> IBookService.CrearLibro(BookRequest bookRequest) {
            var book = new Book
            {
                Name = bookRequest.Name ?? string.Empty,
                Code = bookRequest.Code ?? string.Empty,
            };
            var createBook = await _context.BookEntity.AddAsync(book);
            await _context.SaveChangesAsync();
            return createBook.Entity;
        }
    }
}
