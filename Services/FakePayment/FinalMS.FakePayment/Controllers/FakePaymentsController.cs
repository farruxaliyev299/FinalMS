using FinalMS.FakePayment.DTOs;
using FinalMS.Shared.ControllerBases;
using FinalMS.Shared.DTOs;
using FinalMS.Shared.Messages;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalMS.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : CustomControllerBase
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public FakePaymentsController(ISendEndpointProvider sendEndpointProvider)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> RecievePayment(PaymentDto paymentDto)
        {

            var sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri("queue:create-order"));

            CreateOrderMessageCommand command = new CreateOrderMessageCommand();

            command.BuyerId = paymentDto.Order.BuyerId;
            command.Street = paymentDto.Order.Address.Street;
            command.Province = paymentDto.Order.Address.Province;
            command.Line = paymentDto.Order.Address.Line;
            command.District = paymentDto.Order.Address.District;

            foreach (var item in paymentDto.Order.OrderItems)
            {
                command.Items.Add(new OrderItem
                {
                    PictureUrl = item.PictureUrl,
                    Price = item.Price,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductQuantity = item.ProductQuantity,
                });
            }

            await sendEndpoint.Send<CreateOrderMessageCommand>(command);

            return CreateActionResultInstance(Shared.DTOs.Response<NoContent>.Success(StatusCodes.Status200OK));
        }
    }
}
