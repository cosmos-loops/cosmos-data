using System;

namespace Cosmos.Data.Core.Registrars
{
    /// <summary>
    /// Interface of DbContext configure register
    /// </summary>
    /// <typeparam name="TContainerRegister"></typeparam>
    public interface IDbContextConfigureRegister<out TContainerRegister>
    {
        /// <summary>
        /// Register DbContext
        /// </summary>
        /// <param name="action"></param>
        void RegisterDbContext(Action<TContainerRegister> action);
    }
}