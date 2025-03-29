namespace Application.DTOs.Cart;

public class CartItemDto
{
    public string Id { get; set; } = null!;
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal TotalPrice => Price * Quantity;
}
