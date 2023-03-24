using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.DTOs;
using FinalMS.Shared.DTOs;

namespace FinalMS.Catalog.Services.Stores
{
    public interface IStoreService
    {
        Task<Response<List<StoreDto>>> GetAllAsync();
        Task<Response<StoreDto>> GetByIdAsync(string id);
        Task<Response<StoreDto>> CreateAsync(StoreCreateDto storeDto);
        Task<Response<NoContent>> UpdateAsync(StoreUpdateDto storeDto);
        Task<Response<NoContent>> DeleteAsync(string id);
    }
}
