using System;

using MediatR;

using Microsoft.AspNetCore.Mvc;

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

    // [HttpGet("{supplierId:guid}/{branchId:int}", Name = "GetSupplier")]
    // public async Task<ActionResult> GetSupplier(Guid supplierId, int branchId)
    // {
    //     var supplier = await _mediator.Send(new GetSupplierQuery(supplierId, branchId));
    //     return Ok(supplier);
    // }
}
