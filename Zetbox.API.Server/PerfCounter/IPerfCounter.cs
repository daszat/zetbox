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
namespace Zetbox.API.Server.PerfCounter
{
    using System;
    using System.Collections.Generic;
    using Autofac;
    using Zetbox.API.PerfCounter;

    public interface IPerfCounter : IBasePerfCounter
    {
    }

    public interface IPerfCounterAppender : IBasePerfCounterAppender
    {
    }

    public static class LifetimeScopeExtensions
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Server.PerfCounter.LifetimeScopeExtensions");
        private static readonly object _allocationTraceLock = new object();
        private static readonly List<object> allocationTraces = new List<object>();
        private static int debuggedTraces = 0;

        public static void ApplyPerfCounterTracker(this ILifetimeScope scope)
        {
            scope.ChildLifetimeScopeBeginning += (sender, a) =>
            {
                // recurse down the scope tree
                a.LifetimeScope.ApplyPerfCounterTracker();

                // record stack traces of currently active scopes
                string trace = string.Empty;
                if (Log.IsDebugEnabled) trace = Environment.StackTrace;

                lock (_allocationTraceLock)
                {
                    allocationTraces.Add(trace);
                }
                a.LifetimeScope.CurrentScopeEnding += (s, e) =>
                {
                    lock (_allocationTraceLock)
                    {
                        allocationTraces.Remove(trace);
                        Log.DebugFormat("CurrentScopeEnding: {0} LifetimeScopes remain", allocationTraces.Count);
                        if (Log.IsDebugEnabled && allocationTraces.Count > 10 && debuggedTraces < 100)
                        {
                            foreach (var t in allocationTraces)
                            {
                                Log.Debug("TRACE:");
                                Log.Debug(t);
                                Log.Debug("=========================================");

                                debuggedTraces += 1;
                                if (debuggedTraces > 100)
                                    break;
                            }
                        }
                    }
                };
            };

            // perfCtr measurements
            var perfCtr = scope.Resolve<IPerfCounter>();
            var startTicks = perfCtr.IncrementLifetimeScope();
            scope.CurrentScopeEnding += (s, a) => perfCtr.DecrementLifetimeScope(startTicks);
        }
    }
}
