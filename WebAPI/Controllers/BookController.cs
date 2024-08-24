using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.DataAccess;
using WebAPI.Dtos;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookCreateDto bookDto)
        {

            var book = new Book
            {
                BookName = bookDto.BookName,
                Price = bookDto.Price,
                NumberOfPages = bookDto.NumberOfPages
            };

            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return StatusCode(201);
            //Eger  olusturdugumuz kitaba ihtiyac duyarsak bunu kullanabiliriz.
            //return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            var books = await _context.Books.ToListAsync();
            return books;
        }


        // READ - GET api/book/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return book;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditBook(int id, BookEditDto bookDto)
        {
            // Veritabanında kitabı ID'ye göre bulma
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound(); // Kitap bulunamazsa 404 Not Found döner
            }

            // Mevcut kitabı DTO'dan gelen yeni verilerle güncelle
            book.BookName = bookDto.BookName;
            book.Price = bookDto.Price;
            book.NumberOfPages = bookDto.NumberOfPages;

            // Değişiklikleri veritabanına kaydet
            await _context.SaveChangesAsync();

            return Ok(book); // Başarılı güncelleme sonrası 200 OK döner
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _context.Books.FindAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Book deleted successfully" });
        }
    }
}
