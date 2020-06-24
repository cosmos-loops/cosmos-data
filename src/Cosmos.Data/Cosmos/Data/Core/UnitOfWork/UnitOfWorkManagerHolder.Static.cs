using System;
using System.Threading;
using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    internal partial class UnitOfWorkManagerHolder
    {
        private static AsyncLocal<IUnitOfWorkManager> UnitOfWorkHolder = new AsyncLocal<IUnitOfWorkManager>();

        public static IUnitOfWorkManager Instance
        {
            get
            {
                if (UnitOfWorkHolder.Value is null)
                    UnitOfWorkHolder.Value = new UnitOfWorkManagerHolder();
                return UnitOfWorkHolder.Value;
            }

            set
            {
                if (value is null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                if (UnitOfWorkHolder.Value is null)
                {
                    UnitOfWorkHolder.Value = value;
                    return;
                }

                if (UnitOfWorkHolder.Value.IsVirtualManager && !value.IsVirtualManager)
                {
                    UnitOfWorkHolder.Value = value;
                }

                // 当 UnitOfWorkHolder 的值为 UnitOfWork 时，就不能再进行修改了
            }
        }

        public static bool? CurrentIsVirtual => UnitOfWorkHolder.Value?.IsVirtualManager;
    }
}