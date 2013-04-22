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
    using System.Linq;
    using System.ComponentModel;
    using System.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Server;
    using cal = Zetbox.App.Calendar;
    using Zetbox.API.Utils;

    public class SyncProviderService : IService
    {
        [Feature(NotOnFallback=true)]
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

        private Func<IZetboxServerContext> _ctxFactory;

        public SyncProviderService(Func<IZetboxServerContext> ctxFactory)
        {
            if(ctxFactory == null) throw new ArgumentNullException("ctxFactory");
            _ctxFactory = ctxFactory;
        }

        private const int TIMER_DUE_TIME = 5;
        private const int TIMER_INTERVAL = 10;
        private Timer _timer;
        private static readonly object _lock = new object();
        private bool _isRunning = false; // protection for been called twice

        public void Start()
        {
            _timer = new Timer(_timer_callback, null, TIMER_DUE_TIME * 1000, TIMER_INTERVAL * 1000);
        }

        public void Stop()
        {
            _timer.Dispose();
            _timer = null;
        }

        private void _timer_callback(object state)
        {
            lock (_lock)
            {
                if (_isRunning) return;
                _isRunning = true;
            }
            try
            {
                using (var ctx = _ctxFactory())
                {
                    var now = DateTime.Now;
                    var provider = ctx.GetQuery<cal.SyncProvider>()
                        .Where(i => i.NextSync <= now)
                        .OrderBy(i => i.NextSync)
                        .FirstOrDefault();

                    if (provider != null)
                    {
                        Logging.Server.Info(string.Format("Starting calendar sync on provider {0}", provider.Name));
                        provider.PerformSync();
                        ctx.SubmitChanges();
                        Logging.Server.Info(string.Format("calendar sync finished, next sync will be on the {0}", provider.NextSync));
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Log.Error("Error during calendar sync", ex);
            }
            finally
            {
                // No need for lock -> my thread has set _isRunning to true
                _isRunning = false;
            }
        }

        public string DisplayName
        {
            get { return "Sync provider service"; }
        }

        public string Description
        {
            get { return "Executes sync provider based on their next sync date."; }
        }
    }
}
