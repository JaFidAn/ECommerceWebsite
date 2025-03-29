namespace Application.DTOs.Cart;

public class AddToCartDto
{
    public string ProductId { get; set; } = null!;
    public int Quantity { get; set; }
}
