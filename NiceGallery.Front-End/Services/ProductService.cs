using NiceGallery.Web.Interfaces;
using NiceGallery.Web.Models;
using System.Net.Http.Json;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Product>>("https://localhost:7097/api/produtos");
    }

    public async Task<bool> UpdateProductAsync(Product product)
    {
        var response = await _httpClient.PutAsJsonAsync($"https://localhost:7097/api/produtos/{product.Id}", product);
        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteProductAsync(int productId)
    {
        var response = await _httpClient.DeleteAsync($"https://localhost:7097/api/produtos/{productId}");
        return response.IsSuccessStatusCode;
    }
}