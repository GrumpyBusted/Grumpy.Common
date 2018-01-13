namespace Grumpy.Common.Interfaces
{
    /// <summary>
    /// Generate unique key, this uses Guid in the default implementation, but use class to be able to stub out in test scenarios.
    /// </summary>
    public interface IUniqueKey
    {
        /// <summary>
        /// Generate Unique Key
        /// </summary>
        /// <returns>Key</returns>
        string Generate();
    }
}
