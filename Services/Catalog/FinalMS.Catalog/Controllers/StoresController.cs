using FinalMS.Catalog.DTOs.Create;
using FinalMS.Catalog.DTOs.Update;
using FinalMS.Catalog.Services.Categories;
using FinalMS.Catalog.Services.Stores;
using FinalMS.Shared.ControllerBases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalMS.Catalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoresController : CustomControllerBase
    {
        private readonly IStoreService _storeService;

        public StoresController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _storeService.GetAllAsync();
            return CreateActionResultInstance(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _storeService.GetByIdAsync(id);

            return CreateActionResultInstance(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(StoreCreateDto storeDto)
        {
            var response = await _storeService.CreateAsync(storeDto);

            return CreateActionResultInstance(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(StoreUpdateDto storeDto)
        {
            var response = await _storeService.UpdateAsync(storeDto);

            return CreateActionResultInstance(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _storeService.DeleteAsync(id);

            return CreateActionResultInstance(response);
        }
    }
}
