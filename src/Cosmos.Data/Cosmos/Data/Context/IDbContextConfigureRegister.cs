using System;

namespace Cosmos.Data.Context
{
    public interface IDbContextConfigureRegister<out TContainerRegister>
    {
        TContainerRegister Services { get; }

        void RegisterDbContext(Action<TContainerRegister> action);
    }
}