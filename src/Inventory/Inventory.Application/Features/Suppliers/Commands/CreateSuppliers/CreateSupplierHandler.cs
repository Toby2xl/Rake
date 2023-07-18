using System;

using FluentValidation;

using Inventory.Application.Repository;
using Inventory.Application.Service;
using Inventory.Core.Entities;

using MediatR;

namespace Inventory.Application.Features.Suppliers.Commands.CreateSuppliers;

public class CreateSupplierHandler : IRequestHandler<CreateSupplierCommand, CreateSupplyResponse>
{
    private readonly ITenantService _tenantService;
    private readonly ISupplyRepo _supplyRepo;
    private readonly IValidator<CreateSupplierCommand> _validator;
    public CreateSupplierHandler(ITenantService tenantService, ISupplyRepo supplyRepo, IValidator<CreateSupplierCommand> validator)
    {
        _tenantService = tenantService;
        _supplyRepo = supplyRepo;
        _validator = validator;
    }
    public async Task<CreateSupplyResponse> Handle(CreateSupplierCommand request, CancellationToken cancellationToken)
    {
        const int tenantId =  1;//_tenantService.TenantId;
        int branchId = request.BranchId;
        var response = new CreateSupplyResponse();
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.Errors.Count > 0)
        {
            response.Success = false;
            response.ValidationErrors = new List<string>();
            foreach (var error in validationResult.Errors)
            {
                response.ValidationErrors.Add(error.ErrorMessage);
            }
        }
        var supplier = Supplier.CreateNewSupplier(request.Name, request.Email, request.Address, request.PhoneNumber, tenantId, branchId);
        var created = await _supplyRepo.AddSupplierAsync(supplier, cancellationToken);
        if (!created)
        {
            response.Success = false;
            response.Data = null;
            response.Message = "The Supplier already exists.";
            return response;
        }

        var createSupplier = new CreateSupplierDto(supplier.Id,
                                                 supplier.Name,
                                                 supplier.Email,
                                                 supplier.Address,
                                                 supplier.PhoneNumbers,
                                                 branchId);
        response.Data = createSupplier;
        response.Message = $"Supplier with Name {createSupplier.Name} created successfully";

        return response;
    }
}
