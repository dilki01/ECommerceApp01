using ECommerceApp.Data;
using ECommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApp.Services;

public  class CategoryService
{
    private readonly ApplicationDbContext _db;
    public CategoryService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Category>> GetAllAsync() => await _db.Categories.ToListAsync();

    public async Task<Category?> GetByIdAsync(int id) => await _db.Categories.FindAsync(id);

    public async Task AddAsync(Category category)
    {
        _db.Categories.Add(category);
        await _db.SaveChangesAsync();
    }

    public async Task UpdateAsync(Category category)
    {
        _db.Categories.Update(category);
        await _db.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var cat = await _db.Categories.FindAsync(id);
        if (cat != null)
        {
            _db.Categories.Remove(cat);
            await _db.SaveChangesAsync();
        }
    }
}