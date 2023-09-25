using System.ComponentModel.DataAnnotations;

namespace TheJitu_Commerce_Products.Model.Dtos
{
    public class ProductDto
    {
        
        public Guid ProductId { get; set; }
       
        public string ProductName { get; set; } = string.Empty;
        
        public string ProductDescription { get; set; } = string.Empty;
        
        public double ProductPrice { get; set; }
       
        public string CategoryName { get; set; } = string.Empty;
       
        public string ImageUrl { get; set; } = string.Empty;
    }
}
