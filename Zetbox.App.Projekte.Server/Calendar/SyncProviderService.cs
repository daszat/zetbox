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

namespace Zetbox.App.Projekte.Server.Calendar
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using Zetbox.App.Calendar;

    public class SyncProviderService : IService
    {
        #region Autofac Module
        [Feature(NotOnFallback = true)]
        [Description("Zetbox calendar sync provider service")]
        public class Module : Autofac.Module
        {
            protected override void Load(ContainerBuilder moduleBuilder)
            {
                base.Load(moduleBuilder);

                // Register explicit overrides here
                moduleBuilder
                    .RegisterType<SyncProviderService>()
                    .AsImplementedInterfaces()
                    .SingleInstance();
            }
        }
        #endregion

        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(SyncProviderService));

        private readonly ILifetimeScope _scopeFactory;

        public SyncProviderService(ILifetimeScope scopeFactory)
        {
            if (scopeFactory == null) throw new ArgumentNullException("scopeFactory");
            _scopeFactory = scopeFactory;
        }

        private const int TIMER_DUE_TIME_SEC = 30; // Avoid start during service startup
        private const int TIMER_INTERVAL_SEC = 10;
        private Timer _timer;

        /// <summary>Is true while the _timer_callback is running. Use _isRunningLock to protect access.</summary>
        private bool _isRunning = false;

        /// <summary>The lock for accessing _isRunning.</summary>
        private readonly object _isRunningLock = new object();

        public void Start()
        {
            if (_timer != null)
            {
                Log.Warn("Tried to Start() again. Ignoring.");
                return;
            }

            _timer = new Timer(_timer_callback, null, TIMER_DUE_TIME_SEC * 1000, TIMER_INTERVAL_SEC * 1000);
        }

        public void Stop()
        {
            _timer.Dispose();
            _timer = null;
        }

        private void _timer_callback(object state)
        {
            lock (_isRunningLock)
            {
                if (_isRunning) return;
                _isRunning = true;
            }
            try
            {
                using (var scope = _scopeFactory.BeginLifetimeScope())
                using (var ctx = scope.Resolve<IZetboxServerContext>())
                {
                    var now = DateTime.Now;
                    var provider = ctx.GetQuery<SyncProvider>()
                        .Where(i => i.NextSync <= now)
                        .OrderBy(i => i.NextSync)
                        .FirstOrDefault();

                    if (provider != null)
                    {
                        Log.InfoFormat("Starting calendar sync on provider {0}", provider.Name);
                        provider.PerformSync();
                        ctx.SubmitChanges();
                        Log.InfoFormat("calendar sync finished, next sync will be on the {0}", provider.NextSync);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error during calendar sync", ex);
            }
            finally
            {
                lock (_isRunningLock)
                {
                    _isRunning = false;
                }
            }
        }

        public string DisplayName
        {
            get { return "Calendar SyncProvider Service"; }
        }

        public string Description
        {
            get { return "Executes sync providers based on their next sync date."; }
        }
    }
}
