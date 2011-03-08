using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Kistl.Client.Bootstrapper
{
    public partial class Bootstrapper : Form
    {
        #region Fields
        Thread thread;
        bool running = true;
        string address = string.Empty;
        WebClient service = new WebClient();
        AutoResetEvent downloadEvent = new AutoResetEvent(false);
        string startExec = string.Empty;
        string targetDir = string.Empty;
        bool downloadError = false;
        #endregion

        public Bootstrapper()
        {
            InitializeComponent();
            thread = new Thread(new ThreadStart(run_bootstrapper));
        }

        #region Main Thread-Method
        private void run_bootstrapper()
        {
            try
            {
                running = true;
                var files = LoadFileInformations();
                if (files == null) return;

                targetDir = PrepareTargetDir();

                startExec = string.Empty;

                InitProgressBar(files.Files.Length);

                service.DownloadProgressChanged += new DownloadProgressChangedEventHandler(service_DownloadProgressChanged);
                service.DownloadFileCompleted += new AsyncCompletedEventHandler(service_DownloadFileCompleted);

                for (int i = 0; i < files.Files.Length; i++)
                {
                    if (!running) return;

                    var f = files.Files[i];

                    SetStatus(string.Format("Loading File {0}/{1}: {2}", i + 1, files.Files.Length, f.GetDisplayFileName()));
                    SetProgressBar(i);

                    if (!DownloadFile(f)) return;
                }

                CleanupUnusedFiles(files);

                SetStatus("Starting ZBox");
                StartZBox();

                CloseBootstrapper();
            }
            catch (Exception ex)
            {
                SetStatus("Error: " + ex.Message);
            }
        }
        #endregion

        #region BL
        private FileInfoArray LoadFileInformations()
        {
            SetStatus("Loading Fileinformation");
            var adr = new Uri(new Uri(address), "Bootstrapper.svc/GetFileInfos");
            var filesBuffer = GetFileInfos(adr);

            if (string.IsNullOrEmpty(filesBuffer))
            {
                SetStatus("Could not connect to Service");
                return null;
            }

            return filesBuffer.FromXmlString<FileInfoArray>();
        }

        private string GetFileInfos(Uri adr)
        {
            string filesBuffer = string.Empty;

            if (!string.IsNullOrEmpty(Properties.Settings.Default.UserName))
            {
                service.Credentials = new NetworkCredential(Properties.Settings.Default.UserName, Properties.Settings.Default.Password.Unprotect());
            }

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
                    }
                    return null;
                }
            }
        }

        private string PrepareTargetDir()
        {
            var targetDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            targetDir = Path.Combine(targetDir, "dasz");
            targetDir = Path.Combine(targetDir, "ZBox");
            targetDir = Path.Combine(targetDir, "BootStrapper");
            targetDir = Path.Combine(targetDir, new Uri(address).Authority.GetLegalPathName());

            Directory.CreateDirectory(targetDir);
            return targetDir;
        }

        private bool DownloadFile(FileInfo f)
        {
            var targetFile = f.GetFullFileName(targetDir);

            if (f.Type == FileType.Exec)
            {
                startExec = targetFile;
            }

            if (File.Exists(targetFile))
            {
                var fi = new System.IO.FileInfo(targetFile);
                if (fi.LastWriteTimeUtc == f.Date && fi.Length == f.Size) return true;
            }

            Directory.CreateDirectory(Path.Combine(targetDir, f.DestPath));

            var adr = new Uri(new Uri(address), "Bootstrapper.svc/GetFile/" + f.DestPath + "/" + f.Name);
            service.DownloadFileAsync(adr, targetFile);
            downloadEvent.WaitOne();

            if (!downloadError)
            {
                File.SetCreationTimeUtc(targetFile, f.Date);
                File.SetLastWriteTimeUtc(targetFile, f.Date);
                File.SetLastAccessTimeUtc(targetFile, f.Date);
                return true;
            }
            else
            {
                return false;
            }
        }

        private void CleanupUnusedFiles(FileInfoArray files)
        {
            SetStatus("Cleanup unused files");

            var allFiles = Directory.GetFiles(targetDir, "*.*", SearchOption.AllDirectories);
            var toDelete = allFiles.Except(files.Files.Select(i => i.GetFullFileName(targetDir)));

            foreach (var f in toDelete)
            {
                Debug.WriteLine(String.Format("Deleting {0}", f));
                File.Delete(f);
            }
        }

        private void StartZBox()
        {
            ProcessStartInfo pi = new ProcessStartInfo();
            pi.FileName = startExec;
            pi.WorkingDirectory = Path.GetDirectoryName(startExec);
            Process.Start(pi);
        }
        #endregion

        #region Events
        void service_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            // TODO: proper dialogbox/retry mechanism (Case #2183)
            if (e.Error != null)
            {
                Debug.WriteLine(String.Format("Failed to download {0}", e));
                SetStatus("Download failed:" + e.Error.Message);
                downloadError = true;
            }

            downloadEvent.Set();
        }

        void service_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            if (!running)
            {
                service.CancelAsync();
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => progressBarDownload.Value = (int)e.ProgressPercentage));
            }
            else
            {
                progressBarDownload.Value = (int)e.ProgressPercentage;
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
        #endregion

        #region UI
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
        #endregion
    }
}
