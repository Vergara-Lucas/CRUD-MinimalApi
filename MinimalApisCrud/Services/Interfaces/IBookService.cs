using MinimalApisCrud.Models;

namespace MinimalApisCrud.Services.Interfaces
{
    public interface IBookService
    {
        Task<Book> CrearLibro(BookRequest bookRequest);
    }
}
