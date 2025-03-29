using Application.Core;
using Application.DTOs.Category;
using Application.Repositories.CategoryRepositories;
using Application.Services;
using Application.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;

namespace Persistence.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryReadRepository _readRepository;
    private readonly ICategoryWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public CategoryService(ICategoryReadRepository readRepository, ICategoryWriteRepository writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<CategoryDto>>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        var query = _readRepository
            .GetAll()
            .OrderBy(x => x.CreatedAt)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider);

        var pagedResult = await PagedResult<CategoryDto>.CreateAsync(
            query,
            paginationParams.PageNumber,
            paginationParams.PageSize,
            cancellationToken
        );

        return Result<PagedResult<CategoryDto>>.Success(pagedResult);
    }

    public async Task<Result<CategoryDto?>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var category = await _readRepository.GetByIdAsync(id);
        if (category == null) return Result<CategoryDto?>.Failure(MessageGenerator.NotFound("Category"), 404);

        var dto = _mapper.Map<CategoryDto>(category);
        return Result<CategoryDto?>.Success(dto);
    }

    public async Task<Result<string>> CreateAsync(CreateCategoryDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        await _writeRepository.AddAsync(category);
        await _writeRepository.SaveAsync();
        return Result<string>.Success(category.Id, MessageGenerator.CreationSuccess("Category"));
    }

    public async Task<Result<bool>> UpdateAsync(UpdateCategoryDto dto)
    {
        var category = await _readRepository.GetByIdAsync(dto.Id);
        if (category == null) return Result<bool>.Failure(MessageGenerator.NotFound("Category"), 404);

        category.Name = dto.Name;
        _writeRepository.Update(category);
        await _writeRepository.SaveAsync();

        return Result<bool>.Success(true, MessageGenerator.UpdateSuccess("Category"));
    }

    public async Task<Result<bool>> DeleteAsync(string id)
    {
        var success = await _writeRepository.RemoveAsync(id);
        if (!success) return Result<bool>.Failure(MessageGenerator.DeletionFailed("Category"), 404);

        await _writeRepository.SaveAsync();
        return Result<bool>.Success(true, MessageGenerator.DeletionSuccess("Category"));
    }
}
