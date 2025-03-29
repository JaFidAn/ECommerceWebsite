namespace Domain.Entities;

public class CartItem
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public string CartId { get; set; } = null!;

    // Navigation Properties
    public Cart Cart { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
