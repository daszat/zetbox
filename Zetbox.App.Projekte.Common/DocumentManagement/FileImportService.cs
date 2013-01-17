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

namespace Zetbox.App.Projekte.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using at.dasz.DocumentManagement;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.API.Configuration;
    using System.ComponentModel;

    public class FileImportService : IService
    {
        [Feature(NotOnFallback=true)]
        [Description("Zetbox file import service")]
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                // Register explicit overrides here
                moduleBuilder
                    .RegisterType<FileImportService>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }

        private object _lock = new object();
        private List<FileSystemWatcher> _watcher = new List<FileSystemWatcher>();
        private Func<IZetboxContext> _ctxFactory;
        private Queue<string> _fileQueue = new Queue<string>();
        private Thread _workerThread;
        private bool _isRunning = true;
        private AutoResetEvent _fileEvent;

        public FileImportService(Func<IZetboxContext> ctxFactory)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");

            _ctxFactory = ctxFactory;
        }

        #region IService Members

        public void Start()
        {
            _isRunning = true;
            _fileEvent = new AutoResetEvent(false);

            ThreadPool.QueueUserWorkItem(initFileWatcher);

            // start background thread
            _workerThread = new Thread(backgroundThread);
            _workerThread.Priority = ThreadPriority.BelowNormal;
            _workerThread.IsBackground = true;
            _workerThread.Start();
        }

        public void Stop()
        {
            _isRunning = false;
            foreach (var w in _watcher)
            {
                w.Dispose();
            }
            _watcher.Clear();

            if (!_workerThread.Join(2000))
            {
                _workerThread.Abort();
            }
            _workerThread = null;
            _fileEvent.Close();
            _fileEvent = null;
        }

        public string DisplayName { get { return "Fileimporter"; } }
        public string Description { get { return "Watches a directory and automatically imports new files as Blobs."; } }

        #endregion

        private void initFileWatcher(object state)
        {
            try
            {
                using (var ctx = _ctxFactory())
                {
                    var machine = System.Environment.MachineName.ToLower();

                    var configs = ctx.GetQuery<FileImportConfiguration>()
                                    .Where(i => (i.MachineName.ToLower() == machine)
                                             || (i.MachineName == null))
                                    .ToList();
                    foreach (var cfg in configs)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(cfg.PickupDirectory))
                            {
                                var dir = cfg.PickupDirectory;
                                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\.*%(\w*)%\.*");
                                dir = regex.Replace(dir, (m) => System.Environment.GetEnvironmentVariable(m.Groups[1].Value));

                                if (Directory.Exists(dir))
                                {
                                    var watcher = new FileSystemWatcher(dir, "*.*");
                                    watcher.IncludeSubdirectories = true;
                                    watcher.Created += watcher_Changed;
                                    watcher.Changed += watcher_Changed;
                                    watcher.EnableRaisingEvents = true;

                                    _watcher.Add(watcher);
                                    Logging.Log.InfoFormat("Directory '{0}' added to file watcher", dir);

                                    foreach (var f in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                                    {
                                        Enqueue(f);
                                    }
                                }
                                else
                                {
                                    Logging.Log.WarnFormat("Directory '{0}' does not exists", dir);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Logging.Log.Warn("Error initializing file importer config " + cfg.ToString(), ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Warn("Error initializing file importer", ex);
            }
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Enqueue(e.FullPath);
        }

        private void Enqueue(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                Logging.Log.InfoFormat("Adding '{0}' to queue", file);
                lock (_lock)
                {
                    _fileQueue.Enqueue(file);
                }
                _fileEvent.Set();
            }
        }

        private string Dequeue()
        {
            lock (_lock)
            {
                if (_fileQueue.Count > 0)
                    return _fileQueue.Dequeue();
                else
                    return null;
            }
        }

        public void backgroundThread()
        {
            while (_isRunning)
            {
                _fileEvent.WaitOne(1000);
                while (_isRunning)
                {
                    var file = Dequeue();
                    if (file == null) break;
                    try
                    {
                        ProcessFile(file);
                    }
                    catch (Exception ex)
                    {
                        Logging.Log.Error("Error in FileImport", ex);
                        // Put back to queue, at the end
                        Enqueue(file);
                    }
                }
            }
        }

        private void ProcessFile(string file)
        {
            Logging.Log.InfoFormat("FileImport: processing '{0}'", file);
            if (System.IO.File.Exists(file))
            {
                using (var s = TryGetExclusiveLock(file))
                {
                    if (s != null)
                    {
                        // Upload
                        using (Logging.Log.DebugTraceMethodCall("Uploading file", file))
                        using (var ctx = _ctxFactory())
                        {
                            var blobID = ctx.CreateBlob(s, Path.GetFileName(file), new System.IO.FileInfo(file).GetMimeType());
                            var importedFile = ctx.Create<ImportedFile>();
                            importedFile.Blob = ctx.Find<Blob>(blobID);
                            importedFile.Name = importedFile.Blob.OriginalName;
                            ctx.SubmitChanges();
                        }

                        // Success -> delete file
                        s.Dispose();
                        System.IO.File.Delete(file);
                    }
                    else
                    {
                        Logging.Log.DebugFormat("FileImport: unable to get exclusive lock, putting back to queue");
                        Enqueue(file);
                    }
                }
            }
            else
            {
                Logging.Log.DebugFormat("FileImport: '{0}' has been deleted", file);
            }
        }

        private Stream TryGetExclusiveLock(string file)
        {
            try
            {
                return System.IO.File.Open(file, FileMode.Open, FileAccess.Read, FileShare.None);
            }
            catch
            {
                return null;
            }
        }
    }
}
