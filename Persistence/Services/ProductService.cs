using Application.Core;
using Application.DTOs.Product;
using Application.Repositories.ProductRepositories;
using Application.Services;
using Application.Utilities;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Services;

public class ProductService : IProductService
{
    private readonly IProductReadRepository _readRepository;
    private readonly IProductWriteRepository _writeRepository;
    private readonly IMapper _mapper;

    public ProductService(IProductReadRepository readRepository, IProductWriteRepository writeRepository, IMapper mapper)
    {
        _readRepository = readRepository;
        _writeRepository = writeRepository;
        _mapper = mapper;
    }

    public async Task<Result<PagedResult<ProductDto>>> GetAllAsync(PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        var query = _readRepository
            .GetAll()
            .OrderBy(x => x.CreatedAt)
            .ProjectTo<ProductDto>(_mapper.ConfigurationProvider);

        var pagedResult = await PagedResult<ProductDto>.CreateAsync(
            query,
            paginationParams.PageNumber,
            paginationParams.PageSize,
            cancellationToken
        );

        return Result<PagedResult<ProductDto>>.Success(pagedResult);
    }

    public async Task<Result<ProductDto?>> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var product = await _readRepository
            .GetAll()
            .Where(x => x.Id == id)
            .Include(x => x.Category)
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
            return Result<ProductDto?>.Failure(MessageGenerator.NotFound("Product"), 404);

        var dto = _mapper.Map<ProductDto>(product);
        return Result<ProductDto?>.Success(dto);
    }


    public async Task<Result<string>> CreateAsync(CreateProductDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        await _writeRepository.AddAsync(product);
        await _writeRepository.SaveAsync();
        return Result<string>.Success(product.Id, MessageGenerator.CreationSuccess("Product"));
    }

    public async Task<Result<bool>> UpdateAsync(UpdateProductDto dto)
    {
        var product = await _readRepository.GetByIdAsync(dto.Id);
        if (product == null) return Result<bool>.Failure(MessageGenerator.NotFound("Product"), 404);

        _mapper.Map(dto, product);

        _writeRepository.Update(product);
        await _writeRepository.SaveAsync();

        return Result<bool>.Success(true, MessageGenerator.UpdateSuccess("Product"));
    }

    public async Task<Result<bool>> DeleteAsync(string id)
    {
        var success = await _writeRepository.RemoveAsync(id);
        if (!success) return Result<bool>.Failure(MessageGenerator.DeletionFailed("Product"), 404);

        await _writeRepository.SaveAsync();
        return Result<bool>.Success(true, MessageGenerator.DeletionSuccess("Product"));
    }
}
