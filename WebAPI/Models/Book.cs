using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string BookName { get; set; } = string.Empty;
        [Required]
        public double Price { get; set; }
        [Required]
        public int NumberOfPages { get; set; }
    }
}
