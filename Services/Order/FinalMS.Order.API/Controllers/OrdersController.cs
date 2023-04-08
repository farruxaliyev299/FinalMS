using FinalMS.Order.Application.Commands;
using FinalMS.Order.Application.DTOs;
using FinalMS.Order.Application.Queries;
using FinalMS.Shared.ControllerBases;
using FinalMS.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinalMS.Order.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : CustomControllerBase
{
    private readonly ISharedIdentityService _identityService;
    private readonly IMediator _mediator;

    public OrdersController(ISharedIdentityService identityService, IMediator mediator)
    {
        _identityService = identityService;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersByUserId()
    {
        var response = await _mediator.Send(new GetOrdersByUserIdQuery { UserId = _identityService.GetUserId });
        return CreateActionResultInstance(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
    {
        var response = await _mediator.Send(createOrderCommand);
        return CreateActionResultInstance(response);
    }
}
