using Application.Repositories.OrderRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.OrderRepositories;

public class OrderWriteRepository : WriteRepository<Order>, IOrderWriteRepository
{
    public OrderWriteRepository(ApplicationDbContext context) : base(context) { }
}
