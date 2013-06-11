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
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using at.dasz.DocumentManagement;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

    public class FileImportService : IService
    {
        #region Autofac Module
        [Feature(NotOnFallback = true)]
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
        #endregion

        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.App.Projekte.DocumentManagement.FileImportService");

        private readonly ILifetimeScope _scopeFactory;

        /// <summary>A queue of currently processing files. Access to this Queue is protected by the _fileQueueLock.</summary>
        private Queue<string> _fileQueue = new Queue<string>();
        /// <summary>A queue of deferred processing files. Access to this Queue is protected by the _fileQueueLock.</summary>
        /// <remarks>Files that create an error on uploading (e.g. still being accessed locally) are put here. A timer puts them back onto the _fileQueue, where they'll be processed as usual.</remarks>
        private Queue<string> _deferredFileQueue = new Queue<string>();

        /// <summary>The lock for accessing the FileQueues.</summary>
        /// <remarks>Using a single lock for both Queues makes for simpler programming (no deadlock) and it is not expected that there is much contention from the Timer on this lock.</remarks>
        private readonly object _fileQueueLock = new object();

        private List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();
        private Thread _uploaderThread;

        private const int TIMER_DUE_TIME_SEC = 30; // Avoid start during service startup
        private const int TIMER_INTERVAL_SEC = 10;
        private Timer _deferralTimer;

        /// <summary>This is set to signal the _uploaderThread to shut down.</summary>
        private ManualResetEvent _shutdownEvent;
        /// <summary>This is set to signal the _uploaderThread that new files are available.</summary>
        private AutoResetEvent _fileEvent;

        public FileImportService(ILifetimeScope scopeFactory)
        {
            if (scopeFactory == null) throw new ArgumentNullException("scopeFactory");

            _scopeFactory = scopeFactory;
        }

        #region IService Members

        public void Start()
        {
            if (_shutdownEvent != null)
            {
                Log.Warn("Tried to Start() an already running FileImportService. Ignored.");
                return;
            }
            _shutdownEvent = new ManualResetEvent(false);
            _fileEvent = new AutoResetEvent(false);

            ThreadPool.QueueUserWorkItem(InitialiseFileWatchers);

            // start background thread
            _uploaderThread = new Thread(UploaderThread);
            _uploaderThread.Priority = ThreadPriority.BelowNormal;
            _uploaderThread.IsBackground = true;
            _uploaderThread.Start();

            _deferralTimer = new Timer(state => RetryDeferredFiles(), null, TIMER_DUE_TIME_SEC * 1000, TIMER_INTERVAL_SEC * 1000);
        }

        public void Stop()
        {
            _deferralTimer.Dispose();
            _deferralTimer = null;

            RetryDeferredFiles();

            // signal the _workerThread to shutdown.
            _shutdownEvent.Set();

            // shortcut waiting for new files. If there are any in the queue, they will still be processed.
            _fileEvent.Set();

            foreach (var w in _watchers)
            {
                w.Dispose();
            }
            _watchers.Clear();

            if (!_uploaderThread.Join(2000))
            {
                _uploaderThread.Abort();
            }
            _uploaderThread = null;
            _fileEvent.Close();
            _fileEvent = null;
            _shutdownEvent.Close();
            _shutdownEvent = null;
        }

        public string DisplayName { get { return "Fileimporter"; } }
        public string Description { get { return "Watches a directory and automatically imports new files as Blobs."; } }

        #endregion

        private void InitialiseFileWatchers(object state)
        {
            try
            {
                using (var scope = _scopeFactory.BeginLifetimeScope())
                using (var ctx = scope.Resolve<IZetboxContext>())
                {
                    var machine = Environment.MachineName.ToLower();

                    var configs = ctx.GetQuery<FileImportConfiguration>()
                                    .Where(i => (i.MachineName.ToLower() == machine)
                                             || (i.MachineName == null))
                                    .ToList();

                    // match environment variable references
                    var regex = new Regex(@"\.*%(\w*)%\.*");

                    foreach (var cfg in configs)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(cfg.PickupDirectory))
                            {
                                var dir = cfg.PickupDirectory;
                                dir = regex.Replace(dir, (m) => Environment.GetEnvironmentVariable(m.Groups[1].Value));

                                if (Directory.Exists(dir))
                                {
                                    var watcher = new FileSystemWatcher(dir, "*.*");
                                    watcher.BeginInit();
                                    watcher.IncludeSubdirectories = true;
                                    watcher.Created += watcher_Changed;
                                    watcher.Changed += watcher_Changed;
                                    watcher.EnableRaisingEvents = true;
                                    watcher.EndInit();

                                    _watchers.Add(watcher);
                                    Log.InfoFormat("Now watching directory '{0}'", dir);

                                    foreach (var f in Directory.GetFiles(dir, "*.*", SearchOption.AllDirectories))
                                    {
                                        Enqueue(f);
                                    }
                                }
                                else
                                {
                                    Log.WarnFormat("Directory '{0}' does not exists", dir);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Log.Warn("Error initializing file importer config " + cfg.ToString(), ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Warn("Error initializing file importer", ex);
            }
        }

        private void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            Enqueue(e.FullPath);
        }

        private void Enqueue(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                Log.InfoFormat("Adding '{0}' to queue", file);
                lock (_fileQueueLock)
                {
                    _fileQueue.Enqueue(file);
                }
                _fileEvent.Set();
            }
        }

        private void Defer(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                Log.InfoFormat("Adding '{0}' to deferred queue", file);
                lock (_fileQueueLock)
                {
                    _deferredFileQueue.Enqueue(file);
                }
            }
        }

        private string Dequeue()
        {
            lock (_fileQueueLock)
            {
                if (_fileQueue.Count > 0)
                    return _fileQueue.Dequeue();
                else
                    return null;
            }
        }

        private void RetryDeferredFiles()
        {
            lock (_fileQueueLock)
            {
                if (_deferredFileQueue.Count > 0)
                {
                    Log.InfoFormat("Flushing {0} items from the deferred queue to the processing queue", _deferredFileQueue.Count);
                    while (_deferredFileQueue.Count > 0)
                        _fileQueue.Enqueue(_deferredFileQueue.Dequeue());
                    _fileEvent.Set();
                }
            }
        }

        private void UploaderThread()
        {
            // run until signalled, no waiting
            while (!_shutdownEvent.WaitOne(0))
            {
                // wait until files appear or Stop() is called
                if (_fileEvent.WaitOne(-1))
                {
                    string file;
                    // process all queued files
                    while ((file = Dequeue()) != null)
                    {
                        ProcessFile(file);
                    }
                }
            }
        }

        private void ProcessFile(string file)
        {
            try
            {
                Log.InfoFormat("processing '{0}'", file);
                if (System.IO.File.Exists(file))
                {
                    using (var s = TryGetExclusiveLock(file))
                    {
                        if (s != null)
                        {
                            // Upload
                            using (Log.DebugTraceMethodCall("Uploading file", file))
                            using (var scope = _scopeFactory.BeginLifetimeScope())
                            using (var ctx = scope.Resolve<IZetboxContext>())
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
                            Log.DebugFormat("unable to get exclusive lock");
                            Defer(file);
                        }
                    }
                }
                else
                {
                    Log.DebugFormat("FileImport: '{0}' has been deleted", file);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error in ProcessFile", ex);
                Defer(file);
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
