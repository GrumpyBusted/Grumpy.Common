namespace Grumpy.Common
{
    /// <summary>
    /// Utility Class for Unique Key
    /// </summary>
    public static class UniqueKeyUtility
    {
        /// <summary>
        /// Generate unique key
        /// </summary>
        /// <returns>Key</returns>
        public static string Generate()
        {
            return new UniqueKey().Generate();
        }
    }
}