using System;
using System.Diagnostics;
using System.Threading;

namespace Grumpy.Common.Threading
{
    /// <summary>
    /// Timer utilities - Intended to use in test cases
    /// </summary>
    public static class TimerUtility
    {
        /// <summary>
        /// Wait fon an delegate function to return true. Suggest that you use inline function like TimerUtility.WaitForIt(() => 3 > ++i, 1000)
        /// </summary>
        /// <param name="expression">Call back expression function, e.g. () => 3 > ++i</param>
        /// <param name="millisecondsTimeout">Timeout in milliseconds</param>
        /// <returns>True is expired and false is Timeout</returns>
        public static bool WaitForIt(Func<bool> expression, int millisecondsTimeout)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            var result = expression();

            while (!result && stopwatch.ElapsedMilliseconds < millisecondsTimeout)
            {
                Thread.Sleep(10);

                result = expression();
            }

            return result;
        }

        /// <summary>
        /// Wait fon an delegate function to return true. This might cause infinity look, use overload with timeout.
        /// </summary>
        /// <param name="expression">Call back expression function, e.g. () => 3 > ++i</param>
        public static void WaitForIt(Func<bool> expression)
        {
            var stopwatch = new Stopwatch();

            stopwatch.Start();

            while (!expression())
            {
                Thread.Sleep(10);
            }
        }
    }
}