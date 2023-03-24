using AutoMapper;
using FinalMS.Catalog.DTOs;
using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.Models;
using FinalMS.Catalog.Settings;
using FinalMS.Shared.DTOs;
using MongoDB.Driver;

namespace FinalMS.Catalog.Services.Stores
{
    public class StoreService : IStoreService
    {
        private readonly IMongoCollection<Store> _storeCollection;
        private readonly IMapper _mapper;

        public StoreService(IMapper mapper, IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _storeCollection = database.GetCollection<Store>(settings.StoreCollectionName);
            _mapper = mapper;
        }

        public async Task<Response<List<StoreDto>>> GetAllAsync()
        {
            var stores = await _storeCollection.Find(store => true).ToListAsync();

            return Response<List<StoreDto>>.Success(_mapper.Map<List<StoreDto>>(stores), StatusCodes.Status200OK);
        }
        public async Task<Response<StoreDto>> GetByIdAsync(string id)
        {
            var existingStore = await _storeCollection.Find(store => store.Id == id).FirstOrDefaultAsync();

            if (existingStore is null) return Response<StoreDto>.Fail("Store not found!", StatusCodes.Status404NotFound);

            return Response<StoreDto>.Success(_mapper.Map<StoreDto>(existingStore), StatusCodes.Status200OK);
        }

        public async Task<Response<StoreDto>> CreateAsync(StoreCreateDto storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);

            await _storeCollection.InsertOneAsync(store);

            return Response<StoreDto>.Success(_mapper.Map<StoreDto>(store), StatusCodes.Status201Created);
        }


        public async Task<Response<NoContent>> UpdateAsync(StoreUpdateDto storeDto)
        {
            var updateStore = _mapper.Map<Store>(storeDto);

            var existingProduct = await _storeCollection.FindOneAndReplaceAsync(store => store.Id == storeDto.Id, updateStore);

            if (existingProduct is null) return Response<NoContent>.Fail("Store not found", StatusCodes.Status404NotFound);

            return Response<NoContent>.Success(StatusCodes.Status204NoContent);
        }
        public async Task<Response<NoContent>> DeleteAsync(string id)
        {
            var existingStore = await _storeCollection.DeleteOneAsync(store => store.Id == id);

            if (existingStore.DeletedCount > 0) return Response<NoContent>.Success(StatusCodes.Status204NoContent);

            return Response<NoContent>.Fail("Store not found", StatusCodes.Status404NotFound);
        }
    }
}
