using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using MinimalApisCrud.Context;
using MinimalApisCrud.Models;
using MinimalApisCrud.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BookConnectionString")?? "Data Source=Book.db";
builder.Services.AddDbContext<ApiContext>(opt => opt.UseSqlite(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IBookService,BookServices>();
//forma para usar el InMemory
//builder.Services.AddDbContext<ApiContext>(opt=> opt.UseInMemoryDatabase("api"));
builder.Services.AddSwaggerGen(c =>
    c.SwaggerDoc("v1",new OpenApiInfo { Title="CRUD Books con Minimal Api",Description="Crud de books basico con minimal api y trabajando con metodos asincronicos"})

    );
var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1")
); 

app.MapGet("/api/books", async (ApiContext context) => Results.Ok(await context.BookEntity.ToListAsync()));
app.MapGet("/api/books/{id}", async(int id, ApiContext context) => {
    var book = await context.BookEntity.FindAsync(id);
    if (book is null)
    {
        return Results.NotFound();
    }
    return Results.Ok(book);
});
app.MapPost("/api/book", async(BookRequest request, IBookService bookService) => {
    var crearBook = await bookService.CrearLibro(request);
    return Results.Created($"/books/{crearBook.Id}", crearBook);
});
app.MapDelete("/api/books/{id}", async (int id,ApiContext context) => {
    var book = await context.BookEntity.FindAsync(id);
    if (book is null) {
        return Results.NotFound();
    }
    context.BookEntity.Remove(book);
    await context.SaveChangesAsync();
    //no tiene contenido solo hace 1 accion
    return Results.NoContent();
});
app.MapPut("/api/book", async (int id, BookRequest request,ApiContext context) => {
    var book = await context.BookEntity.FindAsync(id);
    if (book is null)
    {
        return Results.NotFound();
    }
    if (request.Name !=null) {
        book.Name = request.Name;
    }
    if (request.Code != null)
    {
        book.Code = request.Code;
    }
    await context.SaveChangesAsync();
    return Results.Ok(book);
});
app.Run();