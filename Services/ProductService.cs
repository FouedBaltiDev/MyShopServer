using MyShop.Data;

namespace MyShop.Services;

public class ProductService : IProductService
{
    private List<Product> _products = new List<Product>();
    private readonly ApplicationDbContext _dbContext;
    private int _nextId = 1;

    public ProductService(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Product> GetProductByIdAsync(int id)
    {
        return await Task.FromResult(_products.FirstOrDefault(p => p.Id == id));
    }

    public async Task<IEnumerable<Product>> GetAllProductsAsync()
    {
        _products = _dbContext.Products.ToList();
        return await Task.FromResult(_products);
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.Id = _nextId++;
        _products.Add(product);
        return await Task.FromResult(product);
    }

    public async Task<Product> UpdateProductAsync(Product product)
    {
        var existingProduct = _products.FirstOrDefault(p => p.Id == product.Id);
        if (existingProduct != null)
        {
            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
        }
        return await Task.FromResult(existingProduct);
    }

    public async Task<bool> DeleteProductAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
            return await Task.FromResult(true);
        }
        return await Task.FromResult(false);
    }
}

