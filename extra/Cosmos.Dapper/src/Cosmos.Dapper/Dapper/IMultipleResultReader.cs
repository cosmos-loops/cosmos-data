using System.Collections.Generic;

namespace Dapper
{
    public interface IMultipleResultReader
    {
        IEnumerable<T> Read<T>();
    }
}
