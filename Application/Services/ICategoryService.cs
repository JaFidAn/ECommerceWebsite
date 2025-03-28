using Application.Core;
using Application.DTOs.CategoryDtos;

namespace Application.Services;

public interface ICategoryService
{
    Task<Result<PagedResult<CategoryDto>>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken);
    Task<Result<CategoryDto?>> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task<Result<string>> CreateAsync(CreateCategoryDto dto);
    Task<Result<bool>> UpdateAsync(UpdateCategoryDto dto);
    Task<Result<bool>> DeleteAsync(string id);
}
