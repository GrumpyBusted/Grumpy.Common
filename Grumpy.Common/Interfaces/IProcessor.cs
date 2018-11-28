namespace Grumpy.Common.Interfaces
{
    /// <summary>
    /// Processor information
    /// </summary>
    public interface IProcessor
    {
        /// <summary>
        /// Get run cycle number
        /// </summary>
        /// <returns></returns>
        long RunCycle { get; }
    }
}