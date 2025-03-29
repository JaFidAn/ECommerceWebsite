using Application.Repositories.CartRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CartRepositories;

public class CartReadRepository : ReadRepository<Cart>, ICartReadRepository
{
    public CartReadRepository(ApplicationDbContext context) : base(context) { }
}
