namespace ECommerceApp.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public int Stock { get; set; }

    //foreign key to Category
    public int CategoryId { get; set; }

    //Navigation property
    public Category? Category { get; set; } = null!;
}