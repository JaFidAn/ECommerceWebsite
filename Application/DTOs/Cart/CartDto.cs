namespace Application.DTOs.Cart;

public class CartDto
{
    public string Id { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public List<CartItemDto> CartItems { get; set; } = new();
    public decimal TotalPrice => CartItems.Sum(item => item.TotalPrice);
}
