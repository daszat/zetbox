using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Presentables
{
    public class SelectionTaskModel<TChoosable>
        : ViewModel
        where TChoosable : ViewModel
    {
        public new delegate SelectionTaskModel<TChoosable> Factory(IKistlContext dataCtx,
            IList<TChoosable> choices,
            Action<TChoosable> callback,
            IList<CommandModel> additionalActions);

        /// <summary>
        /// Initializes a new instance of the SelectionTaskModel class. This is protected since there 
        /// is no ViewModelDescriptor for this class. Instead, either use the
        /// <see cref="DataObjectSelectionTaskModel"/> or inherit this for a specific type yourself and 
        /// add your own ViewModelDescriptor and View.
        /// </summary>
        /// <param name="appCtx"></param>
        /// <param name="dataCtx"></param>
        /// <param name="choices"></param>
        /// <param name="callback"></param>
        /// <param name="additionalActions"></param>
        protected SelectionTaskModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IList<TChoosable> choices,
            Action<TChoosable> callback,
            IList<CommandModel> additionalActions)
            : base(appCtx, dataCtx)
        {
            _choices = _filteredChoices = new ReadOnlyCollection<TChoosable>(choices);
            _callback = callback;
            _additionalActions = new ReadOnlyCollection<CommandModel>(additionalActions ?? new CommandModel[] { });
        }

        #region Public interface

        public ReadOnlyCollection<TChoosable> Choices
        {
            get
            {
                return _choices;
            }
        }

        public ReadOnlyCollection<TChoosable> FilteredChoices
        {
            get
            {
                return _filteredChoices;
            }
        }

        public ReadOnlyCollection<CommandModel> AdditionalActions
        {
            get
            {
                return _additionalActions;
            }
        }

        public void Choose(TChoosable choosen)
        {
            if (_choices.Contains(choosen))
            {
                _callback(choosen);
            }
            else
            {
                _callback(null);
            }
        }

        private string _filter;
        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter != value)
                {
                    _filter = value;
                    OnFilterChanged();
                }
            }
        }

        protected virtual void OnFilterChanged()
        {
            OnPropertyChanged("Filter");
            FilterChoices();
        }

        private void FilterChoices()
        {
            if (String.IsNullOrEmpty(_filter))
            {
                _filteredChoices = _choices;
            }
            else
            {
                string filter = _filter.ToLowerInvariant();
                _filteredChoices = new ReadOnlyCollection<TChoosable>(_choices.Where(o => o.Name.ToLowerInvariant().Contains(filter)).ToList());
            }
            OnPropertyChanged("FilteredChoices");
        }

        public void Refresh(IList<TChoosable> choices)
        {
            _choices = new ReadOnlyCollection<TChoosable>(choices);
            FilterChoices();
        }

        #endregion

        private ReadOnlyCollection<TChoosable> _choices;
        private ReadOnlyCollection<TChoosable> _filteredChoices;
        private Action<TChoosable> _callback;
        private ReadOnlyCollection<CommandModel> _additionalActions;

        public override string Name
        {
            get { return "Choose object of Type " + typeof(TChoosable).Name; }
        }
    }

    public class DataObjectSelectionTaskModel : SelectionTaskModel<DataObjectModel>
    {
        public new delegate DataObjectSelectionTaskModel Factory(IKistlContext dataCtx, IList<DataObjectModel> choices,
            Action<DataObjectModel> callback,
            IList<CommandModel> additionalActions);

        public DataObjectSelectionTaskModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IList<DataObjectModel> choices,
            Action<DataObjectModel> callback,
            IList<CommandModel> additionalActions)
            : base(appCtx, dataCtx, choices, callback, additionalActions)
        {
        }
    }
}
