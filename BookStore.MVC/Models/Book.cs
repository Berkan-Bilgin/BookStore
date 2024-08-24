namespace BookStore.MVC.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string BookName { get; set; } = "";
        public double Price { get; set; }
        public int NumberOfPages { get; set; }
    }
}
