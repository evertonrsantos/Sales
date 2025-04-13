using SalesApi.Domain.Entities.Base;

namespace SalesApi.Domain.Entities;

public class Product : EntityBase
{
    protected Product() { }

    public Product(string title, decimal price, string description, string category, string image)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        CreatedAt = DateTime.Now;
        IsActive = true;
    }

    public string Title { get; private set; }
    public string Description { get; private set; }
    public string Category { get; private set; }
    public string Image { get; private set; }
    public decimal Price { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public bool IsActive { get; private set; }


    public void Update(string title, decimal price, string description, string category, string image)
    {
        Title = title;
        Price = price;
        Description = description;
        Category = category;
        Image = image;
        UpdatedAt = DateTime.Now;
    }

    public void Deactivate()
    {
        IsActive = false;
        UpdatedAt = DateTime.Now;
    }

    public void Activate()
    {
        IsActive = true;
        UpdatedAt = DateTime.Now;
    }
}
