using System;
using System.Security.Cryptography;
using System.Text;

namespace Inventory.Core.Common;

public static class UPCExtension
{
    public static string GenerateUPC(Guid id)
    {
        var idToHash = id.ToString();
        var hashed = MD5.HashData(Encoding.UTF8.GetBytes(idToHash));
        var idHashedValue = BitConverter.ToInt32(hashed, 0);
        var upcNumber = Math.Abs(idHashedValue);
        return upcNumber.ToString();
    }
}
