using System.Data;
using System;

using MediatR;
using Inventory.Application.Features.Suppliers.Queries.GetSupplier;
using Microsoft.AspNetCore.Mvc;
using Inventory.Application.Features.Suppliers.Queries.GetSupplierList;
using Inventory.Application.Features.Suppliers.Commands.CreateSuppliers;
using Inventory.Application.Features.Suppliers.Commands.UpdateSuppliers;
using Inventory.Application.Features.Suppliers.Commands.DeleteSuppliers;

namespace Inventory.Api.Controllers;

[ApiController]
[Route("api/{tenant}/[controller]")]
public class SupplierController : ControllerBase
{
    private readonly ISender _mediator;
    public SupplierController(ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("")]
    public IResult Get()
    {
        return TypedResults.Ok(" Inventory Supplier Endpoints.......");
    }

    [HttpGet("{supplierId:guid}/branch/{branchId:int}", Name = "GetSupplier")]
    public async Task<IResult> Supplier(Guid supplierId, int branchId)
    {
        var supplier = await _mediator.Send(new GetSupplierQuery(supplierId, branchId));
        return supplier is null ? Results.NotFound($"The resource with Id: {supplierId} not Found")
                                : Results.Ok(supplier);
    }

    [HttpGet("all/branch/{branchId:int}", Name = "GetAllSuppliers")]
    public async Task<IResult> GetAllSuppliers(int branchId)
    {
        var supplierList = await _mediator.Send(new GetSupplierList(branchId));
        return Results.Ok(supplierList);
    }

    [HttpPost(Name = "AddSupplier")]
    public async Task<IResult> Create([FromBody] CreateSupplierCommand request)
    {
        var response = await _mediator.Send(request);
        return Results.Ok(response);
    }

    [HttpPut("{supplierId:guid}/branch/{branchId:int}")]
    public async Task<IResult> Update(Guid supplierId, int branchId, UpdateSupplier command)
    {
        var updateSupplier = new UpdateSupplier(command.Name, command.Email, command.PhoneNumbers, command.Address)
        {
            SupplierId = supplierId,
            BranchId = branchId
        };
        var result = await _mediator.Send(updateSupplier);

        return result.Data is null ? Results.NotFound(result) : Results.Ok(result);
    }

    [HttpDelete("{supplierId:guid}/branch/{branchId:int}")]
    public async Task<IResult> Delete(Guid supplierId, int branchId)
    {
        var result = await _mediator.Send(new DeleteSupplier(supplierId, branchId));
        return result.Data is null ? Results.NotFound(result) : Results.Ok();
    }
}
