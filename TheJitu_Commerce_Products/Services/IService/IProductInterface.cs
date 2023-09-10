using TheJitu_Commerce_Products.Model;

namespace TheJitu_Commerce_Products.Services.IService
{
    public interface IProductInterface
    {
        //Get all products
        Task<IEnumerable<Product>> GetProductsAsync();
        //Get product by Id
        Task<Product> GetProductByIdAsync(Guid productId);
        //Get product by name
        Task<Product> GetProductByNameAsync(string productName);
        //Add product
        Task<string> AddProductAsync(Product product);
        //Update product 
        Task<string> UpdateProductAsync(Product product);
        //Delete product
        Task<string> DeleteProductAsync(Product product);
    }
}
