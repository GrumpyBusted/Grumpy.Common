using System;
using System.Runtime.Serialization;

namespace Grumpy.Common.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Method not Implemented
    /// </summary>
    [Serializable]
    public sealed class MethodNotImplementedException : Exception
    {
        /// <inheritdoc />
        public MethodNotImplementedException(Type type, string methodName) : base($"Method '{methodName}' not implemented in {type.FullName}")
        {
            Data.Add("Type", type.FullName);
            Data.Add("methodName", methodName);
        }
        
        private MethodNotImplementedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}