using System;
using Cosmos.Data.Statements;

namespace Cosmos.Dapper.Core.DataFiltering
{
    public static class DataFilterMixer
    {
        public static ISQLPredicate[] Mix(ISQLPredicate left, ISQLPredicate[] right)
        {
            if (left == null)
                return right;

            var ret = new ISQLPredicate[right.Length + 1];
            ret[0] = left;
            Array.Copy(right, 0, ret, 1, right.Length);
            return ret;
        }
    }
}