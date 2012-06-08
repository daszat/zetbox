// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.WPF
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using Microsoft.Win32.SafeHandles;

    public class DebugConsole
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

            System.Console.WriteLine("Debug Console allocated");
        }
    }
}
