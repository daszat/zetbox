using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.IO;

namespace Kistl.Client.WPF
{
    internal static class DebugConsole
    {
        [DllImport("kernel32.dll",
            EntryPoint = "GetStdHandle",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern IntPtr GetStdHandle(int nStdHandle);
        [DllImport("kernel32.dll",
            EntryPoint = "AllocConsole",
            SetLastError = true,
            CharSet = CharSet.Auto,
            CallingConvention = CallingConvention.StdCall)]
        private static extern int AllocConsole();
        
        private const int STD_OUTPUT_HANDLE = -11;

        private static IntPtr stdHandle;
        private static SafeFileHandle safeFileHandle;
        private static FileStream fileStream;
        private static StreamWriter standardOutput;

        public static void Show()
        {
            AllocConsole();
            stdHandle = GetStdHandle(STD_OUTPUT_HANDLE);
            safeFileHandle = new SafeFileHandle(stdHandle, true);
            fileStream = new FileStream(safeFileHandle, FileAccess.Write);
            standardOutput = new StreamWriter(fileStream, System.Text.Encoding.GetEncoding(437));
            standardOutput.AutoFlush = true;
            System.Console.SetOut(standardOutput);
        }  
    }
}
