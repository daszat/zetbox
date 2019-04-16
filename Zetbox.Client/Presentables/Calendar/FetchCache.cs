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

namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API.Async;
    using Zetbox.API;
    using cal = Zetbox.App.Calendar;

    public sealed class FetchCache
    {
        private struct FetchCacheEntry
        {
            public static FetchCacheEntry None = default(FetchCacheEntry);

            public readonly DateTime FetchTime;
            public readonly ZbTask<List<EventViewModel>> EventsTask;

            public FetchCacheEntry(ZbTask<List<EventViewModel>> events)
            {
                this.FetchTime = DateTime.Now;
                this.EventsTask = events;
            }

            public static bool operator ==(FetchCacheEntry a, FetchCacheEntry b)
            {
                return a.FetchTime == b.FetchTime && a.EventsTask == b.EventsTask;
            }

            public static bool operator !=(FetchCacheEntry a, FetchCacheEntry b)
            {
                return !(a == b);
            }

            public override bool Equals(object obj)
            {
                if (obj is FetchCacheEntry)
                {
                    return this == (FetchCacheEntry)obj;
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return FetchTime.GetHashCode();
            }
        }

        /// <summary>
        /// Remembers all events for the specified day
        /// </summary>
        private readonly SortedList<DateTime, FetchCacheEntry> _cache = new SortedList<DateTime, FetchCacheEntry>();
        private ZbTask<IEnumerable<EventViewModel>> _recurrenceCache = null;
        private readonly List<int> _calendars = new List<int>();

        private readonly IViewModelFactory ViewModelFactory;
        private readonly IZetboxContext _ctx;
        private readonly ViewModel _parent;

        public FetchCache(IViewModelFactory vmf, IZetboxContext ctx, ViewModel parent)
        {
            this.ViewModelFactory = vmf;
            this._ctx = ctx;
            this._parent = parent;
        }

        public void SetCalendars(IEnumerable<int> ids)
        {
            // better implementation necessary
            _recurrenceCache = null;
            _cache.Clear();
            _calendars.Clear();
            _calendars.AddRange(ids);
        }

        public void Invalidate()
        {
            _recurrenceCache = null;
            _cache.Clear();
        }

        public ZbTask<IEnumerable<EventViewModel>> FetchEventsAsync(DateTime from, DateTime to)
        {
            if (_calendars.Count == 0) return new ZbTask<IEnumerable<EventViewModel>>(ZbTask.Synchron, () => new List<EventViewModel>());

            // first -> the recurrence cache
            if (_recurrenceCache == null)
            {
                _recurrenceCache = MakeFetchTask(DateTime.MinValue, DateTime.MinValue);
            }

            // make primary task first
            var result = MakeFetchTask(from, to);
            result.OnResult(t =>
            {
                var range = (to - from);
                MakeFetchTask(from - range, to - range);
                MakeFetchTask(from + range, to + range);
            })
            .OnResult(t =>
            {
                t.Result = t.Result.Union(_recurrenceCache.Result);
            });

            return result;
        }

        private ZbTask<IEnumerable<EventViewModel>> MakeFetchTask(DateTime from, DateTime to)
        {
            var result = new List<ZbTask<List<EventViewModel>>>();

            for (var curDay = from.Date; curDay <= to; curDay = curDay.AddDays(1))
            {
                FetchCacheEntry entry;
                if (_cache.TryGetValue(curDay, out entry))
                {
                    // The SynchronizationContext may change, why is unkown
                    if (entry.FetchTime.AddMinutes(5) > DateTime.Now && entry.EventsTask.SyncContext == System.Threading.SynchronizationContext.Current)
                    {
                        result.Add(entry.EventsTask);
                    }
                    else
                    {
                        _cache.Remove(curDay);
                        entry = FetchCacheEntry.None;
                    }
                }

                if (entry == FetchCacheEntry.None)
                {
                    entry = new FetchCacheEntry(QueryContextAsync(curDay, curDay.AddDays(1)));
                    _cache.Add(curDay, entry);
                    result.Add(entry.EventsTask);
                }
            }

            return new ZbTask<IEnumerable<EventViewModel>>(result)
                .OnResult(t =>
                {
                    t.Result = result.Distinct().SelectMany(r => r.Result).Distinct();
                });
        }

        private ZbTask<List<EventViewModel>> QueryContextAsync(DateTime from, DateTime to)
        {
            var predicateCalendars = GetCalendarPredicate();

            ZbTask<List<cal.Event>> queryTask;
            if (from != DateTime.MinValue)
            {
                queryTask = _ctx.GetQuery<cal.Event>()
                    .Where(predicateCalendars)
                    .Where(e => (e.StartDate >= from && e.StartDate <= to) || (e.EndDate >= from && e.EndDate <= to) || (e.StartDate <= from && e.EndDate >= to))
                    .ToListAsync();
            }
            else
            {
                queryTask = _ctx.GetQuery<cal.Event>()
                    .Where(predicateCalendars)
                    .Where(e => e.Recurrence.Frequency != null)
                    .ToListAsync();
            }

            return new ZbTask<List<EventViewModel>>(queryTask)
                .OnResult(t =>
                {
                    t.Result = queryTask.Result
                        .Select(obj =>
                        {
                            var vmdl = (EventViewModel)DataObjectViewModel.Fetch(ViewModelFactory, _ctx, _parent, obj);
                            vmdl.IsReadOnly = true; // Not changeable. TODO: This should be be implicit. This is a merge server data context
                            return vmdl;
                        })
                        .ToList();
                });
        }

        private System.Linq.Expressions.Expression<Func<cal.Event, bool>> GetCalendarPredicate()
        {
            var predicateCalendars = LinqExtensions.False<cal.Event>();
            foreach (var id in _calendars)
            {
                var localID = id;
                predicateCalendars = predicateCalendars.OrElse<cal.Event>(i => i.Calendar.ID == localID);
            }
            return predicateCalendars;
        }
    }
}
