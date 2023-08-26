using System;
using System.Data;

namespace Identity.Abstractions;

public interface ISqlConnectionFactory
{
    IDbConnection Create();
}
