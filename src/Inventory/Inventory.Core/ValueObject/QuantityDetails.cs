
namespace Inventory.Core.ValueObject;

public class QuantityDetails
{
    public int UpperQuantity { get; set; }
    public string UpperUnits { get; set; } = string.Empty;
    public int LowerQuantity { get; set; }
    public string LowerUnits { get; set; } = string.Empty;
}
