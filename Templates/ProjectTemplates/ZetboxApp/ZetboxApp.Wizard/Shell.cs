using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;

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

        public void ExecuteAsync(string file)
        {
            if (string.IsNullOrEmpty(file)) throw new ArgumentNullException("file");

            _messages.Write(string.Format("Starting {0}\n", file));
            _messages.Write("========================================\n\n");

            System.Diagnostics.ProcessStartInfo psi = new System.Diagnostics.ProcessStartInfo()
            {
                UseShellExecute = false,
                WorkingDirectory = _basePath,
                CreateNoWindow = true,
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = Path.Combine(_basePath, file),
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                RedirectStandardInput = true,
            };

            using (var process = new System.Diagnostics.Process())
            {
                process.StartInfo = psi;
                process.OutputDataReceived += (s, e) => _syncCtx.Post(state => { _messages.WriteLine(e.Data); }, null);
                process.ErrorDataReceived += (s, e) => _syncCtx.Post(state => { _messages.WriteLine(e.Data); }, null);

                process.Start();
                process.BeginErrorReadLine();
                process.BeginOutputReadLine();
                process.Exited += (s, e) => _syncCtx.Post(state =>
                {
                    _messages.Write("========================================\n\n");
                    if (process.ExitCode > 0)
                    {
                        _messages.WriteLine("Process exited with error code {0}", process.ExitCode);
                        MessageBox.Show("Error executing " + file, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }, null);
            }
        }
    }
}
