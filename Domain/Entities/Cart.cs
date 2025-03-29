using Domain.Entities.Common;

namespace Domain.Entities;

public class Cart : BaseEntity
{
    public string UserId { get; set; } = null!;
    public ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();
}
