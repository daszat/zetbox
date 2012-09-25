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

namespace Zetbox.Client.Presentables.ZetboxBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class AuditJournalViewModel : CompoundListViewModel
    {
        public new delegate AuditJournalViewModel Factory(IZetboxContext dataCtx, ViewModel parent, IValueModel mdl);
        private List<KeyValuePair<JournalEntryKey, string>> _journalEntries;

        public override string Name
        {
            get { return "Journal"; }
        }

        public AuditJournalViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ICompoundCollectionValueModel mdl)
            : base(appCtx, dataCtx, parent, mdl)
        {
            var notifier = mdl.UnderlyingCollection as INotifyCollectionChanged;
            if (notifier != null)
            {
                notifier.CollectionChanged += (sender, e) =>
                {
                    _journalEntries = null;
                    OnPropertyChanged("JournalEntries");
                };
            }
        }

        #region Journal

        public sealed class JournalEntryKey
        {
            private readonly string _identity;
            public string Identity { get { return _identity; } }
            private readonly string _timestamp;
            public string Timestamp { get { return _timestamp; } }
            private readonly DateTime _sortKey;
            public DateTime SortKey { get { return _sortKey; } }

            public JournalEntryKey(string identity, DateTime timestamp)
            {
                this._identity = identity;
                this._timestamp = timestamp.ToString();
                this._sortKey = timestamp;
            }

            public override string ToString()
            {
                return string.Format("{0}: {1}", Identity, Timestamp);
            }

            public override bool Equals(object obj)
            {
                var other = obj as JournalEntryKey;

                if (other == null) return false;

                return this.ToString() == other.ToString();
            }

            public override int GetHashCode()
            {
                return this.ToString().GetHashCode();
            }
        }

        public ReadOnlyCollection<KeyValuePair<JournalEntryKey, string>> JournalEntries
        {
            get
            {
                if (_journalEntries == null)
                {
                    var notifier = this.ValueModel.UnderlyingCollection as INotifyCollectionChanged;
                    if (notifier != null)
                        notifier.CollectionChanged += (sender, e) => _journalEntries = null;

                    _journalEntries = ((IEnumerable<AuditEntry>)this.ValueModel.UnderlyingCollection) // cast to correct interface
                        .OfType<AuditEntry>()
                        .GroupBy(e =>
                        {
                            var ts = e.Timestamp.Value;
                            return new JournalEntryKey(e.Identity, new DateTime(ts.Year, ts.Month, ts.Day, ts.Hour, ts.Minute, 0));
                        })
                        .Select(g => new KeyValuePair<JournalEntryKey, string>(g.Key, string.Join("\n", g.Select(e => string.Format(e.MessageFormat, e.PropertyName, e.OldValue, e.NewValue)).ToArray())))
                        .OrderByDescending(p => p.Key.SortKey)
                        .ToList();

                }
                return _journalEntries.AsReadOnly();
            }
        }

        #endregion
    }
}
