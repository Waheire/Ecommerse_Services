using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Products.Model
{
    public class Product
    {
        [Key]
        public Guid ProductId { get; set; }
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string ProductDescription { get; set; } = string.Empty;
        [Range(0, int.MaxValue)]
        public double ProductPrice { get; set; }
        [Required]
        public string CategoryName { get; set; } = string.Empty;
        [Required]
        public string ImageUrl { get; set; } = string.Empty;
    }
}
