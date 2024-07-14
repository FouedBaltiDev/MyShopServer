using Microsoft.AspNetCore.Mvc;
using MyShop.Services;

namespace MyShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        // Simuler une base de données avec une liste statique
        private static List<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Description = "Description1", Price = 100, Stock = 10 },
            new Product { Id = 2, Name = "Product2", Description = "Description2", Price = 200, Stock = 20 },
        };

        // GET: api/Products
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            return Ok(products);
        }

        // GET: api/Products/1
        [HttpGet("{id}")]
        public ActionResult<Product> GetProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<Product> CreateProduct([FromBody] Product product)
        {
            product.Id = products.Count > 0 ? products.Max(p => p.Id) + 1 : 1;
            products.Add(product);
            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // PUT: api/Products/1
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            product.Name = updatedProduct.Name;
            product.Description = updatedProduct.Description;
            product.Price = updatedProduct.Price;
            product.Stock = updatedProduct.Stock;
            return NoContent();
        }

        // DELETE: api/Products/1
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            products.Remove(product);
            return NoContent();
        }



        // Add rows in table products
        // New query => Execute in SSMS with F5

        // INSERT INTO Products (Name, Description, Price, Stock)
        // VALUES
        //('Produit A', 'Description du produit A', 19.99, 100),
        //('Produit B', 'Description du produit B', 29.99, 50),
        //('Produit C', 'Description du produit C', 39.99, 20),
        //('Produit D', 'Description du produit D', 49.99, 75),
        //('Produit E', 'Description du produit E', 59.99, 10);


        // New Methods => Consommation du service Product => ici le code dans le controlleur on l'appelle code client parceque il consomme des services 
        // type de retour de la méthode (async Task<IActionResult>) car o nretourne le résultat d'un appel asynchrone qui est (GetAllProductsAsync)
        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }
    }
}
