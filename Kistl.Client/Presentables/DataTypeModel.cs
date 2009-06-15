
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;

    /// <summary>
    /// Models the specialities of <see cref="DataType"/>s.
    /// </summary>
    public class DataTypeModel
        : DataObjectModel
    {
        /// <summary>
        /// Initializes a new instance of the DataTypeModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="type">the data type to model</param>
        public DataTypeModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            DataType type)
            : base(appCtx, dataCtx, type)
        {
            _type = type;

            // refresh Icon
            UpdateViewCache();
        }

        #region Public interface

        private bool _hasInstances = false;
        public bool HasInstances
        {
            get
            {
                QueryHasInstances();

                // TODO: Wird auch in der Ableitung aufgerufen.
                // Stateobjekt als IDisposable durchreichen
                // aber dann braucht man einen ReferenceCounter - Uaaaaa
                // was nochmals eine andere Klasse von Scheiße ist.
                // Aber man könnte dem einen String umhängen, was geladen wurde.
                // Umpriorisieren & Abbrechen wäre dann auch möglich.
                // Case: 661
                return _hasInstances;
            }
            protected set
            {
                if (value != _hasInstances)
                {
                    _hasInstances = value;
                    OnPropertyChanged("HasInstances");
                }
            }
        }

        // TODO: make readonly, take care of new and deleted+submitted objects
        private ObservableCollection<DataObjectModel> _instances = null;
        public ObservableCollection<DataObjectModel> Instances
        {
            get
            {
                if (_instances == null)
                {
                    _instances = new ObservableCollection<DataObjectModel>();

                    // As this is a "calculated" collection, only insider should modify this
                    ////_instances.CollectionChanged += _instances_CollectionChanged;
                    LoadInstances();
                }
                return _instances;
            }
        }

        private string _instancesSearchString = String.Empty;
        public string InstancesSearchString
        {
            get
            {
                return _instancesSearchString;
            }
            set
            {
                if (_instancesSearchString != value)
                {
                    _instancesSearchString = value;
                    OnInstancesSearchStringChanged(_instancesSearchString);
                }
            }
        }

        private ReadOnlyObservableCollection<DataObjectModel> _instancesFiltered = null;
        public ReadOnlyObservableCollection<DataObjectModel> InstancesFiltered
        {
            get
            {
                if (_instancesFiltered == null)
                {
                    ExecuteFilter();
                }
                return _instancesFiltered;
            }
        }

        #endregion

        #region Utilities and UI callbacks

        /// <summary>
        /// Sets the HasInstances property to the appropriate value
        /// </summary>
        protected virtual void QueryHasInstances()
        {
            HasInstances = false;
        }

        /// <summary>
        /// Loads the instances of this DataType and adds them to the Instances collection
        /// </summary>
        protected virtual void LoadInstances()
        {
            OnInstancesChanged();
        }

        /// <returns>the default icon of this <see cref="DataType"/></returns>
        protected override Kistl.App.GUI.Icon GetIcon()
        {
            if (_type != null)
                return _type.DefaultIcon;
            else
                return null;
        }

        /// <summary>
        /// Call this when the <see cref="Instances"/> property or its 
        /// contents have changed. Override this to react on changes here.
        /// </summary>
        protected virtual void OnInstancesChanged()
        {
            OnPropertyChanged("Instances");
            ExecuteFilter();
        }

        /// <summary>
        /// Call this when the <see cref="InstancesSearchString"/> property 
        /// has changed. Override this to react on changes here.
        /// </summary>
        protected virtual void OnInstancesSearchStringChanged(string oldValue)
        {
            OnPropertyChanged("InstancesSearchString");
            ExecuteFilter();
        }

        /// <summary>
        /// Create a fresh <see cref="InstancesFiltered"/> collection when something has changed.
        /// </summary>
        private void ExecuteFilter()
        {
            if (this.Instances == null)
            {
                _instancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(new ObservableCollection<DataObjectModel>());
            }
            else if (InstancesSearchString == String.Empty)
            {
                _instancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(this.Instances);
            }
            else
            {
                // poor man's full text search
                _instancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(
                    new ObservableCollection<DataObjectModel>(
                        this.Instances.Where(
                            o => o.Name.Contains(this.InstancesSearchString)
                            || o.ID.ToString().Contains(this.InstancesSearchString))));
            }
            OnPropertyChanged("InstancesFiltered");
        }

        #endregion

        private DataType _type;
    }
}
