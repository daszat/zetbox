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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;

    public interface IFileOpener
    {
        void ShellExecute(string filename);
        void ShellExecute(FileInfo fi);
        void ShellExecute(string filename, string verb);
        void ShellExecute(FileInfo fi, string verb);
        string[] GetFileVerbs(string filename);
        string[] GetFileVerbs(FileInfo fi);
    }

    public class DefaultFileOpener : IFileOpener
    {
        public void ShellExecute(string filename)
        {
            ShellExecute(filename, string.Empty);
        }

        public void ShellExecute(FileInfo fi)
        {
            ShellExecute(fi, string.Empty);
        }

        public void ShellExecute(string filename, string verb)
        {
            if (!File.Exists(filename)) throw new ArgumentOutOfRangeException("filename");

            var si = new ProcessStartInfo();
            si.UseShellExecute = true;
            si.FileName = filename;
            si.Verb = verb;
            Process.Start(si);
        }

        public void ShellExecute(FileInfo fi, string verb)
        {
            if (fi == null) throw new ArgumentNullException("fi");
            ShellExecute(fi.FullName, string.Empty);
        }

        public string[] GetFileVerbs(string filename)
        {
            var si = new ProcessStartInfo();
            si.UseShellExecute = true;
            si.FileName = filename;
            return si.Verbs;
        }

        public string[] GetFileVerbs(FileInfo fi)
        {
            if (fi == null) throw new ArgumentNullException("fi");
            return GetFileVerbs(fi.FullName);
        }
    }

    public class NopFileOpener : IFileOpener
    {
        private bool _shellExecWasCalled = false;
        /// <summary>
        /// Returns true if ShellExecute was called and resets the value immediately.
        /// </summary>
        public bool ShellExecWasCalled
        {
            get
            {
                var result = _shellExecWasCalled;
                _shellExecWasCalled = false;
                return result;
            }
        }

        public void ShellExecute(string filename)
        {
            _shellExecWasCalled = true;
        }

        public void ShellExecute(FileInfo fi)
        {
            _shellExecWasCalled = true;
        }

        public void ShellExecute(string filename, string verb)
        {
            _shellExecWasCalled = true;
        }

        public void ShellExecute(FileInfo fi, string verb)
        {
            _shellExecWasCalled = true;
        }

        public string[] GetFileVerbs(string filename)
        {
            return new string[] { };
        }

        public string[] GetFileVerbs(FileInfo fi)
        {
            return new string[] { };
        }
    }
}
