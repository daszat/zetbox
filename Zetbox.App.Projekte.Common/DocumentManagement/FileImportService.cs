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

    public class FileImportService : ThreadedQueueService<string>
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

        private readonly ILifetimeScope _scopeFactory;
        private readonly List<FileSystemWatcher> _watchers = new List<FileSystemWatcher>();

        public FileImportService(ILifetimeScope scopeFactory)
            : base(startupDelay: new TimeSpan(0, 0, 30),
                    retryInterval: new TimeSpan(0, 0, 10),
                    shutdownTimeout: new TimeSpan(0, 0, 2))
        {
            if (scopeFactory == null) throw new ArgumentNullException("scopeFactory");

            _scopeFactory = scopeFactory;
        }

        #region IService Members

        protected override void OnStart()
        {
            base.OnStart();
            ThreadPool.QueueUserWorkItem(InitialiseFileWatchers);
        }

        protected override void OnStop()
        {
            base.OnStop();

            foreach (var w in _watchers)
            {
                w.Dispose();
            }
            _watchers.Clear();
        }

        public override string DisplayName { get { return "Fileimporter"; } }
        public override string Description { get { return "Watches a directory and automatically imports new files as Blobs."; } }

        #endregion

        #region File Watching

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

        #endregion

        protected override bool OnEnqueue(string item)
        {
            // ignore empty file names
            return !string.IsNullOrWhiteSpace(item);
        }

        protected override void ProcessItem(string file)
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
