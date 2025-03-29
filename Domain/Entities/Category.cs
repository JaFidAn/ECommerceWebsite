using Domain.Entities.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = null!;

    // Navigation
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
