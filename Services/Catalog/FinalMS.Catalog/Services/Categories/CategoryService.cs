using AutoMapper;
using FinalMS.Catalog.DTOs;
using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.Models;
using FinalMS.Catalog.Settings;
using MongoDB.Driver;
using FinalMS.Shared.DTOs;

namespace FinalMS.Catalog.Services.Categories;

public class CategoryService : ICategoryService
{
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMapper _mapper;

    public CategoryService(IMapper mapper, IDatabaseSettings settings)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _categoryCollection = database.GetCollection<Category>(settings.CategoryCollectionName);

        _mapper = mapper;
    }

    public async Task<Response<List<CategoryDto>>> GetAllAsync()
    {
        var categories = await _categoryCollection.Find(ctg => true).ToListAsync();

        return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), StatusCodes.Status200OK);
    }

    public async Task<Response<CategoryDto>> GetByIdAsync(string id)
    {
        var existingCategory = await _categoryCollection.Find(category => category.Id == id).FirstOrDefaultAsync();

        if (existingCategory is null) return Response<CategoryDto>.Fail("Category not found!", StatusCodes.Status404NotFound);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(existingCategory), StatusCodes.Status200OK);
    }

    public async Task<Response<CategoryDto>> CreateAsync(CategoryCreateDto categoryDto)
    {
        var category = _mapper.Map<Category>(categoryDto);

        await _categoryCollection.InsertOneAsync(category);

        return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), StatusCodes.Status201Created);
    }

    public async Task<Response<NoContent>> UpdateAsync(CategoryUpdateDto categoryDto)
    {
        var updateCategory = _mapper.Map<Category>(categoryDto);

        var existingCategory = _categoryCollection.ReplaceOne(category => category.Id == categoryDto.Id, updateCategory);

        if (existingCategory.IsAcknowledged is false) return Response<NoContent>.Fail("Category not found", StatusCodes.Status404NotFound);

        return Response<NoContent>.Success(StatusCodes.Status204NoContent);
    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var existingProduct = _categoryCollection.DeleteOne(category => category.Id == id);

        if (existingProduct.DeletedCount > 0) return Response<NoContent>.Success(StatusCodes.Status204NoContent);

        return Response<NoContent>.Fail("Category not found", StatusCodes.Status404NotFound);

    }

}
