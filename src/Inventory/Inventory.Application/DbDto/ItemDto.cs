﻿using Inventory.Core.Entities;
using Inventory.Core.ValueObject;

namespace Inventory.Application;

public class ItemDto
{
    public Item NewItem { get; set; } = default!;
    public QuantityDetails QuantityDetails { get; set; } = null!;
}
