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
using System.Net;

namespace Kistl.Client.Bootstrapper
{
    public partial class Bootstrapper : Form
    {
        Thread thread;
        bool running = true;
        string address = string.Empty;

        public Bootstrapper()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(run_bootstrapper));
        }

        private void run_bootstrapper()
        {
            try
            {
                running = true;

                SetStatus("Connecting to Service");

                SetStatus("Loading Fileinformation");
                WebClient service = new WebClient();

                var adr = new UriBuilder(address);
                adr.Path += "/Bootstrapper.svc/GetFileInfos";
                var filesBuffer = GetFileInfos(adr.Uri, service);

                if (string.IsNullOrEmpty(filesBuffer))
                {
                    SetStatus("Could not connect to Service");
                    return;
                }

                var files = filesBuffer.FromXmlString<FileInfoArray>();

                var targetDir = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                targetDir = Path.Combine(targetDir, "dasz");
                targetDir = Path.Combine(targetDir, "ZBox");
                targetDir = Path.Combine(targetDir, "BootStrapper");
                targetDir = Path.Combine(targetDir, adr.Uri.Authority.GetLegalPathName());

                Directory.CreateDirectory(targetDir);

                string startExec = string.Empty;
                string startConfig = string.Empty;

                InitProgressBar(files.Files.Length);

                for (int i = 0; i < files.Files.Length; i++)
                {
                    if (!running) return;

                    var f = files.Files[i];
                    var targetFile = Path.Combine(targetDir, Path.Combine(f.DestPath, f.Name));

                    SetStatus(string.Format("Loading File {0}/{1}", i + 1, files.Files.Length));
                    SetProgressBar(i);

                    switch (f.Type)
                    {
                        case FileType.Executalable:
                            startExec = targetFile;
                            break;
                        case FileType.AppConfig:
                            startConfig = targetFile;
                            break;
                    }

                    if (File.Exists(targetFile))
                    {
                        var fi = new System.IO.FileInfo(targetFile);
                        if (fi.LastWriteTimeUtc == f.Date && fi.Length == f.Size) continue;
                    }

                    Directory.CreateDirectory(Path.Combine(targetDir, f.DestPath));

                    adr = new UriBuilder(address);
                    adr.Path += "/Bootstrapper.svc/GetFile/" + f.DestPath + "/" + f.Name;
                    service.DownloadFile(adr.Uri, targetFile);

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

        private static string GetFileInfos(Uri adr, WebClient service)
        {
            string filesBuffer = string.Empty;
            while (true)
            {
                try
                {
                    filesBuffer = service.DownloadString(adr);
                    return filesBuffer;
                }
                catch (WebException ex)
                {
                    if (ex.Response is HttpWebResponse && ((HttpWebResponse)ex.Response).StatusCode == HttpStatusCode.Unauthorized)
                    {
                        PasswordDialog dlg = new PasswordDialog();
                        if (dlg.ShowDialog() == DialogResult.OK)
                        {
                            service.Credentials = new NetworkCredential(dlg.UserName, dlg.Password);
                            continue;
                        }
                        else
                        {
                            return null;
                        }
                    }
                }
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
            this.address = Program.Args.Length > 0 ? Program.Args[0] : Properties.Settings.Default.Address;

            if (string.IsNullOrEmpty(address))
            {
                var dlg = new AddressDialog();
                dlg.ShowDialog();
                address = Properties.Settings.Default.Address;
            }

            if (!string.IsNullOrEmpty(address))
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
