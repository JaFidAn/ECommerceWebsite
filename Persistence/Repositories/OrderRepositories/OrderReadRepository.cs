using Application.Repositories.OrderRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.OrderRepositories;

public class OrderReadRepository : ReadRepository<Order>, IOrderReadRepository
{
    public OrderReadRepository(ApplicationDbContext context) : base(context) { }
}
