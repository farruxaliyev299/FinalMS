using AutoMapper;
using FinalMS.Catalog.DTOs;
using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.Models;
using FinalMS.Catalog.Settings;
using MongoDB.Driver;
using FinalMS.Shared.DTOs;
using MT = MassTransit;
using FinalMS.Shared.Messages;

namespace FinalMS.Catalog.Services.Products;

public class ProductService : IProductService
{
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Category> _categoryCollection;
    private readonly IMongoCollection<Store> _storeCollection;
    private readonly MT.IPublishEndpoint _publishEndpoint;
    private readonly IMapper _mapper;

    public ProductService(IMapper mapper, IDatabaseSettings settings, MT.IPublishEndpoint publishEndpoint)
    {
        var client = new MongoClient(settings.ConnectionString);
        var database = client.GetDatabase(settings.DatabaseName);
        _productCollection = database.GetCollection<Product>(settings.ProductCollectionName);
        _categoryCollection = database.GetCollection<Category>(settings.CategoryCollectionName);
        _storeCollection = database.GetCollection<Store>(settings.StoreCollectionName);

        _publishEndpoint = publishEndpoint;
        _mapper = mapper;
    }

    public async Task<Response<List<ProductDto>>> GetAllAsync()
    {
        var products = await _productCollection.Find(product => true).ToListAsync();

        if (products.Any())
        {
            foreach (var product in products)
            {
                product.Category = await _categoryCollection.Find(category => category.Id == product.CategoryId).FirstOrDefaultAsync();
                product.Store = await _storeCollection.Find(store => store.Id == product.StoreId).FirstOrDefaultAsync();
            }
        }

        return Response<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), StatusCodes.Status200OK);
    }

    public async Task<Response<ProductDto>> GetByIdAsync(string id)
    {
        var existingProduct = await _productCollection.Find(product => product.Id == id).FirstOrDefaultAsync();

        if (existingProduct is null) return Response<ProductDto>.Fail("Product not found", StatusCodes.Status404NotFound);

        existingProduct.Category = await _categoryCollection.Find(category => category.Id == existingProduct.CategoryId).FirstOrDefaultAsync();
        existingProduct.Store = await _storeCollection.Find(store => store.Id == existingProduct.StoreId).FirstOrDefaultAsync();

        return Response<ProductDto>.Success(_mapper.Map<ProductDto>(existingProduct), StatusCodes.Status200OK);
    }

    public async Task<Response<List<ProductDto>>> GetAllByStoreIdAsync(string storeId)
    {
        var products = await _productCollection.Find(product => product.StoreId == storeId).ToListAsync();

        if (products.Any())
        {
            foreach (var product in products)
            {
                product.Category = await _categoryCollection.Find(category => category.Id == product.CategoryId).FirstOrDefaultAsync();
                product.Store = await _storeCollection.Find(store => store.Id == product.StoreId).FirstOrDefaultAsync();
            }
        }

        return Response<List<ProductDto>>.Success(_mapper.Map<List<ProductDto>>(products), StatusCodes.Status200OK);
    }

    public async Task<Response<ProductDto>> CreateAsync(ProductCreateDto productDto)
    {
        var product = _mapper.Map<Product>(productDto);

        product.CreatedTime = DateTime.Now;

        await _productCollection.InsertOneAsync(product);

        return Response<ProductDto>.Success(_mapper.Map<ProductDto>(product), StatusCodes.Status201Created);
    }

    //TODO: Fail vermemis metodu bitirir
    public async Task<Response<NoContent>> UpdateAsync(ProductUpdateDto productDto)
    {
        var updateProduct = _mapper.Map<Product>(productDto);

        var existingProduct = await _productCollection.FindOneAndReplaceAsync(product => product.Id == productDto.Id, updateProduct);

        if (existingProduct is null) return Response<NoContent>.Fail("Product not found", StatusCodes.Status404NotFound);

        await _publishEndpoint.Publish<ProductNameChangedEvent>(new ProductNameChangedEvent { ProductId = updateProduct.Id, UpdatedProductName = productDto.Name });

        return Response<NoContent>.Success(StatusCodes.Status204NoContent);

    }

    public async Task<Response<NoContent>> DeleteAsync(string id)
    {
        var existingProduct = await _productCollection.DeleteOneAsync(product => product.Id == id);

        if (existingProduct.DeletedCount > 0) return Response<NoContent>.Success(StatusCodes.Status204NoContent);

        return Response<NoContent>.Fail("Product not found", StatusCodes.Status404NotFound);

    }

}
