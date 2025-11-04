using master_workshop_api.interfaces;
using master_workshop_api.repositories;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace master_workshop_api.controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController: ControllerBase
{
    private readonly ProductsRepository _productRepo;

    public ProductsController(ProductsRepository productRepo)
    {
        _productRepo = productRepo;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _productRepo.GetAllProducts();
        return Ok(products);
        
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _productRepo.GetProductById(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category)
    {
        var result = await _productRepo.GetProductsByCategory(category);
        return Ok(result);
    }

    [HttpGet("low-stock")]
    public async Task<IActionResult> GetLowStock()
    {
        var result = await _productRepo.GetLowStockProducts();
        return Ok(result);
    }
    
    [HttpPut("update")]
    public async Task<IActionResult> UpdateProduct([FromBody]IProduct product)
    {
        var result = await _productRepo.UpdateProduct(product);
        if(result) return Ok(result);
        return BadRequest();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(IProduct product)
    {
        var result = await _productRepo.AddProduct(product);
        if(!result) return BadRequest();
        var list = await _productRepo.GetAllProducts();
        return Ok(list);
    }

    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var result = await _productRepo.DeleteProduct(id);
        if(result) return Ok(result);   
        return BadRequest();
    }
}