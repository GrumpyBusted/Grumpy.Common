using System;
using Grumpy.Common.Interfaces;

namespace Grumpy.Common
{
    public class UniqueKey : IUniqueKey
    {
        public string Generate()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}