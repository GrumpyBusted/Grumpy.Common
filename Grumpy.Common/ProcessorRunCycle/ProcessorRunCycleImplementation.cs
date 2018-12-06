using System;
using System.Runtime.InteropServices;

namespace Grumpy.Common.ProcessorRunCycle
{
    /// <summary>
    /// Static implementation of the Processor Timestamp Assembler function
    /// 
    /// Implementation inspired by:
    /// https://www.intel.com/content/dam/www/public/us/en/documents/white-papers/ia-32-ia-64-benchmark-code-execution-paper.pdf
    /// https://software.intel.com/en-us/articles/introduction-to-x64-assembly/
    /// https://blog.quiscalusmexicanus.org/
    /// https://www.codeproject.com/Members/Nicolai-Nyberg
    /// </summary>
    internal static class ProcessorRunCycleImplementation
    {
        private const uint PageExecuteReadWrite = 0x40;
        private const uint MemoryCommit = 0x1000;

        public static readonly ProcessorRunCycleGetDelegate Get;
        
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAlloc(IntPtr lpAddress, uint dwSize, uint flAllocationType, uint flProtect);
        
        // Calling convention on x64: fastcall, registers rax, rcd, rdx are considered volatile and destroyed on function calls
        private static readonly byte[] Assembler =
        {
            // ReSharper disable once CommentTypo
            0x0F,       // rdtscp              ; edx = hi-dword of TSC, eax = lo-dword of TSC, ecx = ia32_tsc_aux
            0x01, 
            0xF9, 
            0x48,       // shl    rdx,0x20     ; shift left 32 bit (0x20) on rdx, so rdx = hi-dword of TSC shifted into its correct upper 32 bit half
            0xC1, 
            0xE2, 
            0x20, 
            0x48,       // or     rax,rdx      ; bitwise-or the hi-dword of TSC together into rax, which holds the lo-dword of TSC
            0x09, 
            0xD0, 
            0xC3        // ret                 ; return (and the caller picks up rax as the return value, i.e. the full 64 bit TSC value)
        };

        static ProcessorRunCycleImplementation()
        {
            Get = Marshal.GetDelegateForFunctionPointer<ProcessorRunCycleGetDelegate>(Alloc(Assembler));
        }

        private static IntPtr Alloc(byte[] asm)
        {
            var ptr = VirtualAlloc(IntPtr.Zero, (uint)asm.Length, MemoryCommit, PageExecuteReadWrite);
            Marshal.Copy(asm, 0, ptr, asm.Length);
            return ptr;
        }
    }
}