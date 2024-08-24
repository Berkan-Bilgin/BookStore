using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        //türedigimiz class ın constructor ına parametre gonderiyoruz.
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }
    }
}
