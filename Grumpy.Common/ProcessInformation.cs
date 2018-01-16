using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Grumpy.Common.Extensions;
using Grumpy.Common.Interfaces;

namespace Grumpy.Common
{
    /// <inheritdoc />
    public class ProcessInformation : IProcessInformation
    {
        private static class ProcessInformationNativeMethods
        {
            // ReSharper disable once StringLiteralTypo
            [DllImport("WTSAPI32.dll")]
            public static extern bool WTSQuerySessionInformationW(IntPtr hServer, int sessionId, int wtsInfoClass, out IntPtr ppBuffer, out IntPtr pBytesReturned);
        }

        private static readonly IntPtr WtsCurrentServerHandle = IntPtr.Zero;
        private const int WtsInitialProgram = 0;
        private const int WtsApplicationName = 1;
        private const int WtsWorkingDirectory = 2;
        private const int WtsUserName = 5;
        private const int WtsDomainName = 7;

        /// <inheritdoc />
        public string MachineName { get; }

        /// <inheritdoc />
        public string UserName { get; }

        /// <inheritdoc />
        public string DomainName { get; }

        /// <inheritdoc />
        public string WorkingDirectory { get; }

        /// <inheritdoc />
        public string ApplicationName { get; }

        /// <inheritdoc />
        public string InitialProgram { get; }

        /// <inheritdoc />
        public string ProcessName { get; }

        /// <inheritdoc />
        public int Id { get; }

        /// <inheritdoc />
        public ProcessInformation()
        {
            var process = Process.GetCurrentProcess();

            MachineName = (Environment.MachineName.NullOrWhiteSpace() ? process.MachineName : Environment.MachineName).ToUpper();
            Id = process.Id;
            ProcessName = process.ProcessName;
            UserName = GetSessionInformation(Process.GetCurrentProcess(), WtsUserName).ToUpper();

            if (UserName.NullOrWhiteSpace())
                UserName = Environment.UserName;

            DomainName = GetSessionInformation(Process.GetCurrentProcess(), WtsDomainName)?.ToUpper() ?? "";
            WorkingDirectory = GetSessionInformation(Process.GetCurrentProcess(), WtsWorkingDirectory) ?? "";
            ApplicationName = GetSessionInformation(Process.GetCurrentProcess(), WtsApplicationName) ?? "";
            InitialProgram = GetSessionInformation(Process.GetCurrentProcess(), WtsInitialProgram) ?? "";
        }

        private static string GetSessionInformation(Process process, int informationType)
        {
            return ProcessInformationNativeMethods.WTSQuerySessionInformationW(WtsCurrentServerHandle, process.SessionId, informationType, out var answerBytes, out var _) ? Marshal.PtrToStringUni(answerBytes) : null;
        }
    }
}