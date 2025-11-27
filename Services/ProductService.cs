using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace ECommerceApp.Services;

public class ProductService
{
    private readonly ApplicationDbContext _db;
    public ProductService(ApplicationDbContext db)
    {
        _db = db;
    }

    // include Category. so Product.Index can show category name
    public async Task<List<Product>> GetAllAsync() =>
        await _db.Products.Include(p => p.Category).ToListAsync();

    // public async Task<List<Product>> GetAllAsync() => await _db.Products.ToListAsync();

    // public async Task<Product?> GetByIdAsync(int id) => await _db.Products.FindAsync(id);

    public async Task<Product?> GetByIdAsync(int id) =>
        await _db.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);


    public async Task AddAsync(Product product)
    {
        _db.Products.Add(product);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Product product)
    {
        _db.Products.Update(product);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var product = await _db.Products.FindAsync(id);
        if (product != null)
        {
            _db.Products.Remove(product);
            await _db.SaveChangesAsync();
        }
    }
}
