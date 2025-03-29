using Application.Core;
using Application.DTOs.Product;

namespace Application.Services;

public interface IProductService
{
    Task<Result<PagedResult<ProductDto>>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    Task<Result<ProductDto?>> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Result<string>> CreateAsync(CreateProductDto dto);
    Task<Result<bool>> UpdateAsync(UpdateProductDto dto);
    Task<Result<bool>> DeleteAsync(string id);
}
