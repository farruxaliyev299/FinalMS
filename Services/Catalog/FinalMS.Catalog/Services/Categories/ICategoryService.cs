using FinalMS.Catalog.DTOs;
using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.Models;
using FinalMS.Shared.DTOs;

namespace FinalMS.Catalog.Services.Categories;

public interface ICategoryService
{
    Task<Response<List<CategoryDto>>> GetAllAsync();
    Task<Response<CategoryDto>> GetByIdAsync(string id);
    Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryDto);
    Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categoryDto);
    Task<Response<NoContent>> DeleteAsync(string id);



}
