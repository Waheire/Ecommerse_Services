using Microsoft.EntityFrameworkCore;
using TheJitu_Commerce_Products.Data;
using TheJitu_Commerce_Products.Model;
using TheJitu_Commerce_Products.Services.IService;

namespace TheJitu_Commerce_Products.Services
{
    public class ProductService : IProductInterface
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> AddProductAsync(Product product)
        {
           _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return "Product Added Successfully"; 
        }

        public async Task<string> DeleteProductAsync(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return "Product Deleted Successfully";
        }

        public async Task<Product> GetProductByIdAsync(Guid productId)
        {
            return await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
        }

        public async Task<Product> GetProductByNameAsync(string productName)
        {
            return await _context.Products.Where(p => p.ProductName == productName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<string> UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return "Product Updated Successfully";
        }
    }
}
