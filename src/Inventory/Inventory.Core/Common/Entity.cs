using System;

namespace Inventory.Core.Common;

public abstract class Entity<T>
{
    public T? Id { get; set; }
}
