using System;

using Inventory.Application.Features.Store.Commands.CreateStore;
using Inventory.Application.Features.Store.Commands.DeleteStore;
using Inventory.Application.Features.Store.Commands.UpdateStore;
using Inventory.Application.Features.Store.Queries.GetStore;
using Inventory.Application.Features.Store.Queries.GetStoreList;

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

    [HttpGet("{storeId:guid}/branch/{branchId:int}", Name = "GetStore")]
    public async Task<IResult> GetStore(Guid storeId, int branchId)
    {
        var store = await _mediator.Send(new GetStoreQuery(storeId, branchId));
        return store is null ? Results.NotFound() : Results.Ok(store);
    }

    [HttpGet("all/branch/{branchId:int}", Name = "GetAllStores")]
    public async Task<IResult> GetAllStores(int branchId)
    {
        var storesList = await _mediator.Send(new GetStoreListQuery(branchId));
        return Results.Ok(storesList);
    }

    [HttpPost(Name = "AddWarehouse")]
    public async Task<IResult> Create(CreateStoreCommand request)
    {
        var response = await _mediator.Send(request);
        return Results.Ok(response);
    }

    [HttpPut("{storeId:guid}/branch/{branchId:int}")]
    public async Task<IResult> Update(Guid storeId, int branchId, [FromBody] UpdateStoreCommand command)
    {
        command.StoreId = storeId;
        command.BranchId = branchId;
        return Results.Ok(await _mediator.Send(command));
    }

    [HttpDelete("{storeId:guid}/branch/{branchId:int}")]
    public async Task<IResult> Delete(Guid storeId, int branchId)
    {
        return Results.Ok(await _mediator.Send(new DeleteStoreCommand(storeId, branchId)));
    }

    //Items Section...........






}
