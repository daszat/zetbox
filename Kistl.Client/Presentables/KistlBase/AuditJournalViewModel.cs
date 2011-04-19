
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Models;
    using Kistl.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class AuditJournalViewModel : CompoundListViewModel
    {
        public new delegate AuditJournalViewModel Factory(IKistlContext dataCtx, IValueModel mdl);
        private readonly IList<ICompoundObject> _journal;
        private List<KeyValuePair<JournalEntryKey, string>> _journalEntries;

        public override string Name
        {
            get { return "Journal"; }
        }

        public AuditJournalViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            ICompoundCollectionValueModel mdl)
            : base(appCtx, dataCtx, mdl)
        {
            _journal = mdl.Value;
            var notifier = _journal as INotifyCollectionChanged;
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
            public JournalEntryKey(string identity, DateTime timestamp)
            {
                this._identity = identity;
                this._timestamp = timestamp.ToString();
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
                    var notifier = _journal as INotifyCollectionChanged;
                    if (notifier != null)
                        notifier.CollectionChanged += (sender, e) => _journalEntries = null;

                    _journalEntries = _journal
                        .OfType<AuditEntry>()
                        .GroupBy(e =>
                        {
                            var ts = e.Timestamp.Value;
                            return new JournalEntryKey(e.Identity, new DateTime(ts.Year, ts.Month, ts.Day, ts.Hour, ts.Minute, 0));
                        })
                        .Select(g => new KeyValuePair<JournalEntryKey, string>(g.Key, string.Join("\n", g.Select(e => string.Format(e.MessageFormat, e.PropertyName, e.OldValue, e.NewValue)).ToArray())))
                        .OrderByDescending(p => p.Key.Timestamp)
                        .ToList();

                }
                return _journalEntries.AsReadOnly();
            }
        }

        #endregion
    }
}
