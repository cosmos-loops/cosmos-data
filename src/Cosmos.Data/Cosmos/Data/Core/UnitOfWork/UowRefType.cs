namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// Type of UoW-Ref
    /// </summary>
    public enum UowRefType
    {
        /// <summary>
        /// Original<br />
        /// 标记该工作单元为原始工作单元
        /// </summary>
        Original,

        /// <summary>
        /// Virtual<br />
        /// 标记该工作单元为虚拟工作单元，其操作实际上由其内含的原始工作单元来执行
        /// </summary>
        Virtual,

        /// <summary>
        /// Nothing<br />
        /// 标记该工作单元不执行任何操作
        /// </summary>
        Nothing
    }
}