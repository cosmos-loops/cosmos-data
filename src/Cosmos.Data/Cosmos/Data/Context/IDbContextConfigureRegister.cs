using System;

namespace Cosmos.Data.Context
{
    /// <summary>
    /// Interface of DbContext configure register
    /// </summary>
    /// <typeparam name="TContainerRegister"></typeparam>
    public interface IDbContextConfigureRegister<out TContainerRegister>
    {
        /// <summary>
        /// Gets services
        /// </summary>
        TContainerRegister Services { get; }

        /// <summary>
        /// Register DbContext
        /// </summary>
        /// <param name="action"></param>
        void RegisterDbContext(Action<TContainerRegister> action);
    }
}