using Application.Core;
using Application.DTOs.Category;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class CategoriesController : BaseApiController
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] PaginationParams paginationParams, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllAsync(paginationParams, cancellationToken);
        return HandleResult(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);
        return HandleResult(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCategoryDto dto)
    {
        var result = await _categoryService.CreateAsync(dto);
        return HandleResult(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update(UpdateCategoryDto dto)
    {
        var result = await _categoryService.UpdateAsync(dto);
        return HandleResult(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var result = await _categoryService.DeleteAsync(id);
        return HandleResult(result);
    }
}
