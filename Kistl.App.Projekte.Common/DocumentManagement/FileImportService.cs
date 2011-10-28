namespace Kistl.App.Projekte.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Autofac;
    using System.Threading;
    using at.dasz.DocumentManagement;
    using Kistl.API.Common;
using System.IO;
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
        IIdentityResolver _idResolver;

        public FileImportService(Func<IKistlContext> ctxFactory, IIdentityResolver idResolver)
        {
            if (ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            if (idResolver == null) throw new ArgumentNullException("idResolver");

            _ctxFactory = ctxFactory;
            _idResolver = idResolver;
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
                //var id = _idResolver.GetCurrent();

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
                            dir = regex.Replace(dir, new System.Text.RegularExpressions.MatchEvaluator((m) => System.Environment.GetEnvironmentVariable(m.Groups[1].Value)));

                            if (Directory.Exists(dir))
                            {
                                var watcher = new FileSystemWatcher(dir);
                                watcher.IncludeSubdirectories = true;
                                watcher.Created += new FileSystemEventHandler(watcher_Created);
                                _watcher.Add(watcher);
                                Logging.Log.InfoFormat("Directory '{0}' added to file watcher", dir);
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

        void watcher_Created(object sender, FileSystemEventArgs e)
        {
            using (var ctx = _ctxFactory())
            {

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
