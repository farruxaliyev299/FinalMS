using FinalMS.Basket.DTOs;
using FinalMS.Shared.DTOs;

namespace FinalMS.Basket.Services.Basket
{
    public interface IBasketService
    {
        Task<Response<BasketDto>> GetBasket(string userId);
        Task<Response<bool>> SaveOrUpdate(BasketDto basketDto);
        Task<Response<bool>> Delete(string userId);
    }
}
