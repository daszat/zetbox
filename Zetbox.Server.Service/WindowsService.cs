using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using Zetbox.API;

namespace Zetbox.Server.Service
{
    partial class WindowsService : ServiceBase
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.Service");

        private IServiceControlManager _scm;
        public WindowsService(IServiceControlManager scm)
        {
            this._scm = scm;
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);
            StartService();
        }

        protected override void OnStop()
        {
            base.OnStop();
            StopService();
        }

        public void StartService()
        {
            Log.Info("Starting zetbox Services");
            _scm.Start();
        }

        public void StopService()
        {
            _scm.Stop();
        }
    }
}
