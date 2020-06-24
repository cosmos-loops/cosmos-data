namespace Cosmos.Data.Common
{
    /// <summary>
    /// An operator of unit of work.<br />
    /// When the user operates the unit of work, it will be operated through IUnitOfWorkEntry,
    /// but its real work will be performed by the implementation of this interface.
    /// </summary>
    public interface IUnitOfWorkOperator { }
}