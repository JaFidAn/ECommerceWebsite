using Application.Repositories.ProductRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ProductRepositories;

public class ProductReadRepository : ReadRepository<Product>, IProductReadRepository
{
    public ProductReadRepository(ApplicationDbContext context) : base(context) { }
}
