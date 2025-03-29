using Domain.Entities.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string CategoryId { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int Stock { get; set; }

    // Navigation
    public Category Category { get; set; } = null!;
}
