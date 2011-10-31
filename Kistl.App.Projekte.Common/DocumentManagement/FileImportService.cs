namespace Kistl.App.Projekte.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using at.dasz.DocumentManagement;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Utils;

    [ServiceDescriptor]
    public class FileImportService : Kistl.API.IService
    {
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                // Register explicit overrides here
                moduleBuilder
                    .RegisterType<FileImportService>()
                    .SingleInstance();
            }
        }

        private Func<IKistlContext> _ctxFactory;

        public FileImportService(Func<IKistlContext> ctxFactory)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");

            _ctxFactory = ctxFactory;
        }

        public void Start()
        {
            ThreadPool.QueueUserWorkItem(start_serivce);
        }

        private List<FileSystemWatcher> _watcher = new List<FileSystemWatcher>();

        private void start_serivce(object state)
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
                                    AddToQueue(f);
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
                        Logging.Log.Warn("Error initializing file importer", ex);
                    }
                }
            }
        }

        void watcher_Changed(object sender, FileSystemEventArgs e)
        {
            AddToQueue(e.FullPath);
        }

        private static void AddToQueue(string file)
        {
            if (!string.IsNullOrEmpty(file))
            {
                Logging.Log.InfoFormat("TODO: adding '{0}' to queue", file);
            }
        }

        public void Stop()
        {
            foreach (var w in _watcher)
            {
                w.Dispose();
            }
            _watcher.Clear();
        }
    }
}
