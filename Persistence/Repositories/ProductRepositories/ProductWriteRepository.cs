using Application.Repositories.ProductRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.ProductRepositories;

public class ProductWriteRepository : WriteRepository<Product>, IProductWriteRepository
{
    public ProductWriteRepository(ApplicationDbContext context) : base(context) { }
}
