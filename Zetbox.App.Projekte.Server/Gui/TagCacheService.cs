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

namespace Zetbox.App.Projekte.Server.Gui
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.API.Server;

    /// <summary>
    /// Recreates periodically the tag cache. Will be registrated automatically by the CustomServerActionsModule.
    /// </summary>
    public class TagCacheService : IService
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(typeof(TagCacheService));

        private readonly ILifetimeScope _scopeFactory;

        public TagCacheService(ILifetimeScope scopeFactory)
        {
            if (scopeFactory == null) throw new ArgumentNullException("scopeFactory");
            _scopeFactory = scopeFactory;
        }

        private const int TIMER_DUE_TIME_SEC = 45; // Avoid start during service startup
        private const int TIMER_INTERVAL_SEC = 12 * 3600; // twice a day is good enough
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
                Log.Info("Rebuilding tag cache");
                using (var scope = _scopeFactory.BeginLifetimeScope())
                using (var ctx = scope.Resolve<IZetboxServerContext>())
                {
                    var oneTag = ctx.GetQuery<Zetbox.App.GUI.TagCache>().FirstOrDefault();
                    if (oneTag == null)
                    {
                        oneTag = ctx.Create<Zetbox.App.GUI.TagCache>();
                    }
                    oneTag.Rebuild();
                    if (string.IsNullOrWhiteSpace(oneTag.Name))
                        ctx.Delete(oneTag);

                    var changes = ctx.SubmitChanges();
                    Log.InfoFormat("Tag cache rebuilt, submitted {0} changes", changes);
                }
            }
            catch (Exception ex)
            {
                Log.Error("Error during tag cache sync", ex);
            }
            finally
            {
                lock (_isRunningLock)
                {
                    _isRunning = false;
                }
            }
        }

        public string DisplayName { get { return "TagCacheService"; } }
        public string Description { get { return "Recreates periodically the tag cache"; } }
    }
}
