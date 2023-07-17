using System;

using Inventory.Application.Features.Store.Commands.CreateStore;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/{tenant}/[controller]")]
public class StoreController : ControllerBase
{
    private readonly ISender _mediator;
    public StoreController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public IResult Get()
    {
        //await Task.Delay(TimeSpan.FromSeconds(1));
        //return Ok($" Inventory Warehouse API for {_tenantService.Tenant} .........");
        return Results.Ok("Inventory Store API for Demo Purposes....");
    }

    // [HttpGet("{storeId:guid}/{branchId:int}", Name = "GetStore")]
    // public async Task<ActionResult> GetStore(Guid storeId, int branchId)
    // {
    //     var store = await _mediator.Send(new GetStoreQuery(storeId, branchId));
    //     return Ok(store);
    // }

    [HttpPost(Name = "AddWarehouse")]
    public async Task<IResult> Create(CreateStoreCommand request)
    {
        var response = await _mediator.Send(request);
        return Results.Ok(response);
    }
    
}
