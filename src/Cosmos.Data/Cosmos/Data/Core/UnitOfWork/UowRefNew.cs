using System;
using System.Data;
using Cosmos.Data.Common;

namespace Cosmos.Data.Core.UnitOfWork
{
    /// <summary>
    /// UowRefNew helper
    /// </summary>
    public static class UowRefNew
    {
        /// <summary>
        /// Nothing
        /// </summary>
        /// <param name="isNotSupported"></param>
        /// <param name="onDispose"></param>
        /// <returns></returns>
        public static UowRef Nothing(bool isNotSupported, Action<UowRef> onDispose)
        {
            var entry = new NothingUowProxy();
            var uowRef = new UowRef(entry, UowRefType.Nothing, isNotSupported);
            entry.OnDispose = () => onDispose?.Invoke(uowRef);
            return uowRef;
        }

        /// <summary>
        /// Virtual
        /// </summary>
        /// <param name="baseUowEntry"></param>
        /// <param name="isNotSupported"></param>
        /// <param name="onDispose"></param>
        /// <returns></returns>
        public static UowRef Virtual(IUnitOfWorkEntry baseUowEntry, bool isNotSupported, Action<UowRef> onDispose)
        {
            var entry = new VirtualUowProxy(baseUowEntry);
            var uowRef = new UowRef(entry, UowRefType.Virtual, isNotSupported);
            entry.OnDispose = () => onDispose?.Invoke(uowRef);
            return uowRef;
        }

        /// <summary>
        /// Original
        /// </summary>
        /// <param name="baseUowEntry"></param>
        /// <param name="isolationLevel"></param>
        /// <param name="onDispose"></param>
        /// <returns></returns>
        public static UowRef Original(IUnitOfWorkEntry baseUowEntry, IsolationLevel? isolationLevel, Action<UowRef> onDispose)
        {
            var entry = new OriginalUowProxy(baseUowEntry);
            var uowRef = new UowRef(entry, UowRefType.Original, false);
            if (isolationLevel is not null) entry.IsolationLevel = isolationLevel.Value;

            try
            {
                entry.GetOrBegin();
            }
            catch
            {
                entry.Dispose();
                throw;
            }

            entry.OnDispose = () => onDispose?.Invoke(uowRef);

            return uowRef;
        }
    }
}