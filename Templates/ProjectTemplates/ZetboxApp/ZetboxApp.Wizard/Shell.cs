using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace ZetboxApp.Wizard
{
    public class Shell
    {
        private Messages _messages;
        private string _basePath;
        private SynchronizationContext _syncCtx;

        public Shell(Messages messages, string basePath)
        {
            _messages = messages;
            _basePath = basePath;
            _syncCtx = SynchronizationContext.Current;
        }

        private Process ExecuteCore(string file, string arguments)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException("file");

            _messages.Write(string.Format("Starting {0} {1}\n", file, arguments));

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo()
            {
                UseShellExecute = false,
                WorkingDirectory = _basePath,
                CreateNoWindow = true,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = Path.Combine(_basePath, file),
                Arguments = arguments,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
            };

            var process = new System.Diagnostics.Process();
            process.StartInfo = psi;
            process.OutputDataReceived += (s, e) => _syncCtx.Post(state => { _messages.WriteLine(e.Data); }, null);
            process.ErrorDataReceived += (s, e) => _syncCtx.Post(state => { _messages.WriteLine(e.Data); }, null);

            process.Start();
            process.BeginErrorReadLine();
            process.BeginOutputReadLine();
            return process;
        }


        public int Execute(string file, string arguments = null)
        {
            using (var process = ExecuteCore(file, arguments))
            {
                process.WaitForExit();
                return process.ExitCode;
            }
        }

        public void ExecuteAsync(string file, string arguments = null)
        {
            var process = ExecuteCore(file, arguments);
            process.Exited += (s, e) => _syncCtx.Post(state =>
            {
                if (process.ExitCode > 0)
                {
                    _messages.WriteLine("Process exited with error code {0}", process.ExitCode);
                    MessageBox.Show("Error executing " + file, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                process.Dispose();
            }, null);
        }
    }
}
