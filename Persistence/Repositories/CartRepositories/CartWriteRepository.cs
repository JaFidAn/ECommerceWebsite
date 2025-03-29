using Application.Repositories.CartRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CartRepositories;

public class CartWriteRepository : WriteRepository<Cart>, ICartWriteRepository
{
    public CartWriteRepository(ApplicationDbContext context) : base(context) { }
}
