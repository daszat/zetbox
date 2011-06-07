
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
    public abstract class PropertyGroupViewModel
        : ViewModel, IDataErrorInfo
    {
        public new delegate PropertyGroupViewModel Factory(IKistlContext dataCtx, ViewModel parent, string title, IEnumerable<ViewModel> obj);

        private string _title;
        protected ObservableCollection<ViewModel> properties;

        public PropertyGroupViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            string title,
            IEnumerable<ViewModel> obj)
            : base(appCtx, dataCtx, parent)
        {
            _title = title;
            properties = new ObservableCollection<ViewModel>(obj);
            properties.CollectionChanged += PropertyListChanged;
            foreach (var prop in properties)
            {
                prop.PropertyChanged += AnyPropertyChangedHandler;
            }
        }

        #region Public Interface

        public string Title { get { return _title; } }
        public string ToolTip { get { return _title; } }

        public override string Name
        {
            get { return Title; }
        }

        private ReadOnlyObservableCollection<ViewModel> _propertyModelsCache;
        public ReadOnlyObservableCollection<ViewModel> PropertyModels
        {
            get
            {
                if (_propertyModelsCache == null)
                {
                    _propertyModelsCache = new ReadOnlyObservableCollection<ViewModel>(properties);
                }
                return _propertyModelsCache;
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
                        return String.Join("\n", properties.OfType<IDataErrorInfo>().Select(idei => idei.Error).Where(s => !String.IsNullOrEmpty(s)).ToArray());
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

    public class SinglePropertyGroupViewModel : PropertyGroupViewModel
    {
        public new delegate SinglePropertyGroupViewModel Factory(IKistlContext dataCtx, ViewModel parent, string title, IEnumerable<ViewModel> obj);

        public SinglePropertyGroupViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            string title,
            IEnumerable<ViewModel> obj)
            : base(appCtx, dataCtx, parent, title, obj)
        {
        }

        public ViewModel PropertyModel
        {
            get
            {
                return PropertyModels.Single();
            }
        }

    }

    public class MultiplePropertyGroupViewModel : PropertyGroupViewModel
    {
        public new delegate MultiplePropertyGroupViewModel Factory(IKistlContext dataCtx, ViewModel parent, string title, IEnumerable<ViewModel> obj);

        public MultiplePropertyGroupViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            string title,
            IEnumerable<ViewModel> obj)
            : base(appCtx, dataCtx, parent, title, obj)
        {
        }
    }
}
