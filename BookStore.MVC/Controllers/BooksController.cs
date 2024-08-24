using BookStore.MVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.MVC.Controllers
{
    public class BooksController : Controller
    {

        private readonly HttpClient _httpClient;

        public BooksController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Book> books = null;
            try
            {
                books = await _httpClient.GetFromJsonAsync<IEnumerable<Book>>("https://localhost:7068/api/Book");

            }
            catch (HttpRequestException httpEx)
            {

            }

            catch (Exception ex)
            {

            }

            //using (var httpClient = new HttpClient())
            //{
            //    using (var gelenYanit = await httpClient.GetAsync("https://localhost:7079/Books")) ;

            //    {
            //        string gelenString = gelenYanit.Content.ReadAsStringAsync();


            //    }
            //}

            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {


                var response = await _httpClient.PostAsJsonAsync("https://localhost:7068/api/Book", book);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                    //return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, " An error occured while creating the Book");
                }
            }

            return View(book);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            // API'den kitabın mevcut bilgilerini al
            var book = await _httpClient.GetFromJsonAsync<Book>($"https://localhost:7068/api/Book/{id}");

            if (book == null)
            {
                return NotFound(); // Kitap bulunamazsa 404 döndür
            }

            // Kitap bilgilerini view'a gönder
            return View(book);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = await _httpClient.PutAsJsonAsync($"https://localhost:7068/api/Book/{id}", book);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An error occured white updating the book.");
                }
            }

            return View(book);
        }

        public async Task<IActionResult> Details(int id)
        {
            var bookDetails = await _httpClient.GetFromJsonAsync<Book>($"https://localhost:7068/api/Book/{id}");

            if (bookDetails == null)
            {
                return NotFound();
            }

            return View(bookDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _httpClient.GetFromJsonAsync<Book>($"https://localhost:7068/api/Book/{id}");

            if (book == null)
            {
                return NotFound();
            }

            return View(book); // Silinecek kitabı onay sayfasında göster
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _httpClient.DeleteAsync($"https://localhost:7068/api/Book/{id}");


            //response kodunun 200 ile 299 arasında olup olmadıgını kontrol eder.
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return BadRequest();
            }
        }








    }
}
