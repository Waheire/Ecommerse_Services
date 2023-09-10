using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Products.Model.Dtos
{
    public class ProductRequestDto
    {
        [Required]
        public string ProductName { get; set; } = string.Empty;
        [Required]
        public string ProductDescription { get; set; } = string.Empty;
        [Required]
        public int ProductPrice { get; set; }
    }
}
