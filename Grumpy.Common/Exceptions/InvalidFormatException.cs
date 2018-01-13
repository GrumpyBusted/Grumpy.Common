using System;
using System.Runtime.Serialization;

namespace Grumpy.Common.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    /// Invalid format exception
    /// </summary>
    [Serializable]
    public sealed class InvalidFormatException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        /// The format is not supported
        /// </summary>
        /// <param name="field"></param>
        public InvalidFormatException(string field) : base("The format is not supported")
        {
            Data.Add("Field", field);
        }

        private InvalidFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}