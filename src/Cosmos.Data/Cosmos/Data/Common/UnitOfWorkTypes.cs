namespace Cosmos.Data.Common
{
    /// <summary>
    /// Unit of work propagation type
    /// 事务传播方式
    /// </summary>
    public enum UnitOfWorkTypes
    {
        /// <summary>
        /// Required <br />
        /// 如果当前没有事务，就新建一个事务，如果已存在一个事务中，加入到这个事务中，默认的选择。
        /// </summary>
        Required,

        /// <summary>
        /// Supports <br />
        /// 支持当前事务，如果没有当前事务，就以非事务方法执行。
        /// </summary>
        Supports,

        /// <summary>
        /// Mandatory <br />
        /// 使用当前事务，如果没有当前事务，就抛出异常。
        /// </summary>
        Mandatory,

        /// <summary>
        /// NotSupported <br />
        /// 以非事务方式执行操作，如果当前存在事务，就把当前事务挂起。
        /// </summary>
        NotSupported,

        /// <summary>
        /// Never <br />
        /// 以非事务方式执行操作，如果当前事务存在则抛出异常。
        /// </summary>
        Never,

        /// <summary>
        /// Nested <br />
        /// 以嵌套事务方式执行。
        /// </summary>
        Nested
    }
}