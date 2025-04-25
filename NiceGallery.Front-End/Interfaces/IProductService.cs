using NiceGallery.Web.Models;

namespace NiceGallery.Web.Interfaces
{
    public interface IProductService
    {
        Task<List<Product>> GetProductsAsync();
        Task<bool> UpdateProductAsync(Product product);
        Task<bool> DeleteProductAsync(int productId);
    }
}
