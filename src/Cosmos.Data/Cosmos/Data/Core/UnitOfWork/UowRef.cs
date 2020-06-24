using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// UoW-Ref
    /// </summary>
    public class UowRef
    {
        /// <summary>
        /// UoW-Ref
        /// </summary>
        /// <param name="entry"></param>
        /// <param name="type"></param>
        /// <param name="isNotSupported"></param>
        public UowRef(IUnitOfWorkEntry entry, UowRefType type, bool isNotSupported)
        {
            Entry = entry;
            Type = type;
            IsNotSupported = isNotSupported;
        }

        /// <summary>
        /// Entry
        /// </summary>
        public IUnitOfWorkEntry Entry { get; set; }

        /// <summary>
        /// Type
        /// </summary>
        public UowRefType Type { get; set; }

        /// <summary>
        /// Is not supported
        /// </summary>
        public bool IsNotSupported { get; set; }
    }
}