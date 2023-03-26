using FinalMS.Discount.DTOs;
using FinalMS.Discount.DTOs.Create;
using FinalMS.Discount.DTOs.Update;
using FinalMS.Shared.DTOs;

namespace FinalMS.Discount.Services;

public interface IDiscountService
{
    Task<Response<List<DiscountDto>>> GetAll();
    Task<Response<DiscountDto>> GetById(int id);
    Task<Response<NoContent>> Create(DiscountCreateDto discountDto);
    Task<Response<NoContent>> Update(DiscountUpdateDto discountDto);
    Task<Response<NoContent>> Delete(int id);
    Task<Response<DiscountDto>> GetByCodeAndUserId(string code, string userId);
}
