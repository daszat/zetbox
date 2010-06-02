
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;

    /// <summary>
    /// Models a group of Property(Models)
    /// </summary>
    public class PropertyGroupModel
        : ViewModel, IDataErrorInfo
    {
        public new delegate PropertyGroupModel Factory(IKistlContext dataCtx, string title, IEnumerable<ViewModel> obj);

        private string _title;
        private ObservableCollection<ViewModel> _properties;

        public PropertyGroupModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string title,
            IEnumerable<ViewModel> obj)
            : base(appCtx, dataCtx)
        {
            _title = title;
            _properties = new ObservableCollection<ViewModel>(obj);
            _properties.CollectionChanged += PropertyListChanged;
            foreach (var prop in _properties)
            {
                prop.PropertyChanged += AnyPropertyChangedHandler;
            }
        }

        #region Public Interface

        public string Title { get { return _title; } }
        public string ToolTip { get { return _title; } }

        private ReadOnlyObservableCollection<ViewModel> _propertyModelsCache;
        public ReadOnlyObservableCollection<ViewModel> PropertyModels
        {
            get
            {
                if (_propertyModelsCache == null)
                {
                    _propertyModelsCache = new ReadOnlyObservableCollection<ViewModel>(_properties);
                }
                return _propertyModelsCache;
            }
        }

        public override string Name
        {
            get { return Title; }
        }

        public bool LastChildFill
        {
            get
            {
                return _properties.Count == 1;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get { return this["PropertyModels"]; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "Title":
                    case "PropertyModels":
                        return String.Join("\n", PropertyModels.OfType<IDataErrorInfo>().Select(idei => idei.Error).Where(s => !String.IsNullOrEmpty(s)).ToArray());
                    default:
                        return null;
                }
            }
        }

        #endregion

        #region Event handlers

        private void PropertyListChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (var prop in e.NewItems.OfType<INotifyPropertyChanged>())
                {
                    prop.PropertyChanged += AnyPropertyChangedHandler;
                }
            }

            if (e.OldItems != null)
            {
                foreach (var prop in e.OldItems.OfType<INotifyPropertyChanged>())
                {
                    prop.PropertyChanged -= AnyPropertyChangedHandler;
                }
            }
        }

        private void AnyPropertyChangedHandler(object sender, EventArgs e)
        {
            OnPropertyChanged("Title");
            OnPropertyChanged("PropertyModels");
        }

        #endregion
    }
}
