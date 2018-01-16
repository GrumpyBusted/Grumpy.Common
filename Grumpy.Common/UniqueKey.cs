using System;
using Grumpy.Common.Interfaces;

namespace Grumpy.Common
{
    /// <inheritdoc />
    public class UniqueKey : IUniqueKey
    {
        /// <inheritdoc />
        public string Generate()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}