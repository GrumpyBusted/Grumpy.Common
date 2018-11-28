using FluentAssertions;
using System;
using System.Diagnostics;
using System.Threading;
using Xunit;

namespace Grumpy.Common.UnitTests
{
    public class ProcessorTimestampTest
    {
        [Fact]
        public void ManualDriverTest()
        {
            var cut = new Processor();
            var runCycleStart = cut.RunCycle;
            Thread.Sleep(3000);
            var runCycleEnd = cut.RunCycle;
            var delta = runCycleEnd - runCycleStart;
            var cyclesPerSec = 1024 * Stopwatch.Frequency;
            var elapsed = (double)delta / cyclesPerSec;
            
            Console.WriteLine($"{runCycleStart:x16} {runCycleStart}");
            Console.WriteLine($"{runCycleEnd:x16} {runCycleEnd}");
            Console.WriteLine($"{delta:x16} {delta}");
            Console.WriteLine($"Cycles per sec:  {cyclesPerSec}");
            Console.WriteLine($"Elapsed:         {elapsed:F10}s");
        }

        [Fact]
        public void ShouldNotBeZeroTest()
        {
            var cut = new Processor();
            
            var tsc1 = cut.RunCycle;
            
            tsc1.Should().NotBe(0);
        }

        [Fact]
        public void AlwaysBiggerTest()
        {
            var cut = new Processor();
            
            var tsc1 = cut.RunCycle;
            var tsc2 = cut.RunCycle;

            tsc2.Should().BeGreaterThan(tsc1);
        }
    }
}