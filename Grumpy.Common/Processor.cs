using Grumpy.Common.Interfaces;
using Grumpy.Common.ProcessorRunCycle;

namespace Grumpy.Common
{
    /// <inheritdoc />
    public class Processor : IProcessor
    {
        /// <inheritdoc />
        public long RunCycle => ProcessorRunCycleImplementation.Get();
    }
}