using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.DTOs;
using FinalMS.Shared.DTOs;

namespace FinalMS.Catalog.Services.Products
{
    public interface IProductService
    {
        Task<Response<List<ProductDto>>> GetAllAsync();
        Task<Response<ProductDto>> GetByIdAsync(string id);
        Task<Response<List<ProductDto>>> GetAllByStoreIdAsync(string storeId);
        Task<Response<ProductDto>> CreateAsync(ProductCreateDto categoryDto);
        Task<Response<NoContent>> UpdateAsync(ProductUpdateDto categoryDto);
        Task<Response<NoContent>> DeleteAsync(string id);

    }
}
