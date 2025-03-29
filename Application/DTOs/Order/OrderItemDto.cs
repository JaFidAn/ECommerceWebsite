namespace Application.DTOs.Order;

public class OrderItemDto
{
    public string ProductId { get; set; } = null!;
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => Price * Quantity;
}
