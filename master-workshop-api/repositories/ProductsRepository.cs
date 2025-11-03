

using master_workshop_api.interfaces;

namespace master_workshop_api.repositories;

public class ProductsRepository
{
    private List<IProduct> _products = new List<IProduct>();
    private int _nextId = 1;

    public ProductsRepository()
    {
        AddSampleProducts();
    }
    
    private async void AddSampleProducts()
    {
        await AddProduct(new IProduct
        {
            Nombre = "Laptop Dell",
            Descripcion = "Laptop i5 8GB RAM",
            Precio = 899.99m,
            Cantidad = 10,
            Categoria = "Tecnología"
        });

        await AddProduct(new IProduct
        {
            Nombre = "Mouse Inalámbrico",
            Descripcion = "Mouse ergonómico",
            Precio = 25.50m,
            Cantidad = 3,
            Categoria = "Accesorios"
        });
    }
    
    //CREATE
    public async Task<bool> AddProduct(IProduct product)
    {
        product.Id = _nextId;
        _nextId++;
        _products.Add(product);
        Console.WriteLine($"Product: {product.Id} - {product.Nombre} ");
        return true;
    }
    
    //READ ALL
    public async Task<List<IProduct>> GetAllProducts()
    {
        return _products;
    }
    //READ BY ID
    public async Task<IProduct> GetProductById(int id)
    {
        return _products.FirstOrDefault(p => p.Id == id);
    }
    
    // UPDATE
    public async Task<bool> UpdateProduct(IProduct product)
    {
        var existingProduct = await GetProductById(product.Id);
        if (existingProduct == null)
        {
            return false;
        }
        
        existingProduct.Nombre = product.Nombre;
        existingProduct.Descripcion = product.Descripcion;
        existingProduct.Precio = product.Precio;
        existingProduct.Cantidad = product.Cantidad;
        existingProduct.Categoria = product.Categoria;
        return true;
    }
    
    //DELETE
    public async Task<bool>  DeleteProduct(int id)
    {
        var product = await GetProductById(id);
        if (product != null)
        {
            _products.Remove(product);
            return true;
        }
        return false;
    }
    
    //SEARCH BY CATEGORY
    public async Task<List<IProduct>> GetProductsByCategory(string category)
    {
        return _products.Where(p => p.Categoria == category).ToList();
    }
    
    //LOW STOCK PRODUCTS
    public async Task<List<IProduct>> GetLowStockProducts(int minStock = 5)
    {
        return _products.Where(p => p.Cantidad < minStock).ToList();
    }
}