using Application.Repositories.CategoryRepositories;
using Domain.Entities;
using Persistence.Contexts;

namespace Persistence.Repositories.CategoryRepositories;

public class CategoryWriteRepository : WriteRepository<Category>, ICategoryWriteRepository
{
    public CategoryWriteRepository(ApplicationDbContext context) : base(context) { }
}
