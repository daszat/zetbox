using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Security;
using System.Diagnostics;

namespace Kistl.Client.Bootstrapper
{
    public partial class Bootstrapper : Form
    {
        BootstrapperServiceReference.IBootstrapperService service;
        Thread thread;
        bool running = true;

        public Bootstrapper()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(run_bootstrapper));
        }

        public static string GetLegalPathName(string path)
        {
            Path.GetInvalidPathChars().Union(new[] { ':' }).ToList().ForEach(c => path = path.Replace(c, '_'));
            return path;
        }

        private void run_bootstrapper()
        {
            try
            {
                running = true;

                SetStatus("Connecting to Service");
                var adr = new Uri(new Uri(Properties.Settings.Default.Address), "Bootstrapper");

                service = new BootstrapperServiceReference.BootstrapperServiceClient("BasicHttpBinding_IBootstrapperService", adr.AbsoluteUri);
                if (!running) return;

                SetStatus("Loading Fileinformation");
                var files = service.GetFileInfos();

                var targetDir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                targetDir = Path.Combine(targetDir, "dasz");
                targetDir = Path.Combine(targetDir, "ZBox");
                targetDir = Path.Combine(targetDir, "BootStrapper");
                targetDir = Path.Combine(targetDir, GetLegalPathName(adr.Authority));

                Directory.CreateDirectory(targetDir);

                string startExec = string.Empty;
                string startConfig = string.Empty;

                InitProgressBar(files.Length);

                for (int i = 0; i < files.Length; i++)
                {
                    if (!running) return;

                    var f = files[i];
                    var targetFile = Path.Combine(targetDir, Path.Combine(f.DestPath, f.Name));

                    SetStatus(string.Format("Loading File {0}/{1}", i + 1, files.Length));
                    SetProgressBar(i);

                    switch (f.Type)
                    {
                        case BootstrapperServiceReference.FileType.Executalable:
                            startExec = targetFile;
                            break;
                        case BootstrapperServiceReference.FileType.AppConfig:
                            startConfig = targetFile;
                            break;
                    }

                    if (File.Exists(targetFile))
                    {
                        FileInfo fi = new FileInfo(targetFile);
                        if (fi.LastWriteTimeUtc == f.Date && fi.Length == f.Size) continue;
                    }

                    Directory.CreateDirectory(Path.Combine(targetDir, f.DestPath));

                    var data = service.GetFile(f.DestPath, f.Name);

                    using (var fs = File.Create(targetFile))
                    {
                        fs.SetLength(0);
                        fs.Write(data, 0, data.Length);
                    }

                    File.SetCreationTimeUtc(targetFile, f.Date);
                    File.SetLastWriteTimeUtc(targetFile, f.Date);
                    File.SetLastAccessTimeUtc(targetFile, f.Date);
                }

                SetStatus("Starting ZBox");
                StartZBox(startExec, startConfig);

                CloseBootstrapper();
            }
            catch (Exception ex)
            {
                SetStatus("Error: " + ex.Message);
            }
        }

        private void SetProgressBar(int i)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => progressBar.Value = i));
            }
            else
            {
                progressBar.Value = i;
            }
        }

        private void InitProgressBar(int max)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => progressBar.Maximum = max));
            }
            else
            {
                progressBar.Maximum = max;
            }
        }

        private void CloseBootstrapper()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(Close));
            }
            else
            {
                Close();
            }
        }

        private void StartZBox(string exec, string config)
        {
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = exec;
            pi.WorkingDirectory = Path.GetDirectoryName(exec);
            Process.Start(pi);
        }

        private void SetStatus(string txt)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => lbStatus.Text = txt));
            }
            else
            {
                lbStatus.Text = txt;
            }
        }

        private void Bootstrapper_Load(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.Address))
            {
                var dlg = new AddressDialog();
                dlg.ShowDialog();
            }

            if (!string.IsNullOrEmpty(Properties.Settings.Default.Address))
            {
                thread.Start();
            }
            else
            {
                SetStatus("No URL specified");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            running = false;
            this.Close();
        }
    }
}
