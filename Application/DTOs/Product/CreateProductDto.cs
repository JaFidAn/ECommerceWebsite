namespace Application.DTOs.Product;

public class CreateProductDto
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = null!;
    public int Stock { get; set; }
    public string CategoryId { get; set; } = null!;
}
