
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Linq.Dynamic;
    using System.Text;
    using ObjectEditorWorkspace = Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel;

    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.App.Base;
    using Kistl.API.Configuration;
    using System.Reflection;

    /// <summary>
    /// Models the specialities of <see cref="DataType"/>s.
    /// </summary>
    public class InstanceListViewModel
        : ViewModel, IViewModelWithIcon
    {
        public new delegate InstanceListViewModel Factory(IKistlContext dataCtx, DataType type);

        protected readonly Func<IKistlContext> ctxFactory;

        /// <summary>
        /// Initializes a new instance of the DataTypeModel class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="config"></param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="type">the data type to model</param>
        /// <param name="ctxFactory"></param>
        public InstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig config,
            IKistlContext dataCtx,
            DataType type,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
        {
            _type = type;
            this.ctxFactory = ctxFactory;
        }

        #region Public interface

        private List<IFilterExpression> _filter = null;
        public ICollection<IFilterExpression> Filter
        {
            get
            {
                if (_filter == null)
                {
                    _filter = new List<IFilterExpression>();
                }
                return _filter;
            }
        }

        public IEnumerable<IUIFilterExpression> FilterViewModels
        {
            get
            {
                if (_filter == null)
                {
                    return null;
                }
                else
                {
                    return _filter.OfType<IUIFilterExpression>();
                }
            }
        }

        private Kistl.Client.Presentables.DataTypeModel _dataTypeMdl = null;
        public Kistl.Client.Presentables.DataTypeModel DataTypeModel
        {
            get
            {
                if (_dataTypeMdl == null)
                {
                    _dataTypeMdl = ModelFactory.CreateViewModel<DataTypeModel.Factory>(_type).Invoke(DataContext, _type);
                }
                return _dataTypeMdl;
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

        /// <summary>
        /// Reload instances from context.
        /// </summary>
        public void ReloadInstances()
        {
            if (_instances != null)
            {
                _instances.Clear();
                LoadInstances();
                ExecuteFilter();
            }
        }

        /// <summary>
        /// Gets the ID of the modelled DataType.
        /// </summary>
        public int TypeId
        {
            get { return _type.ID; }
        }

        public InterfaceType InterfaceType
        {
            get
            {
                return DataContext.GetInterfaceType(_type.GetDataType());
            }
        }

        public void OpenObject(IEnumerable<DataObjectModel> objects)
        {
            if (objects == null) throw new ArgumentNullException("objects");

            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(ctxFactory());
            foreach (var item in objects)
            {
                newWorkspace.ShowForeignModel(item);
            }
            ModelFactory.ShowModel(newWorkspace, true);
        }

        public void NewObject()
        {
            var newCtx = ctxFactory();
            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            var newObj = newCtx.Create(this.InterfaceType);
            newWorkspace.ShowForeignModel(ModelFactory.CreateViewModel<DataObjectModel.Factory>(newObj).Invoke(newCtx, newObj));
            ModelFactory.ShowModel(newWorkspace, true);
        }

        public void EditClass()
        {
            var newCtx = ctxFactory();
            var objClass = newCtx.Find<DataType>(this.TypeId);
            var newWorkspace = ModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(newCtx);
            newWorkspace.ShowForeignModel(ModelFactory.CreateViewModel<DataObjectModel.Factory>(objClass).Invoke(newCtx, objClass));
            ModelFactory.ShowModel(newWorkspace, true);
        }

        #endregion

        #region Utilities and UI callbacks
        /// <returns>the default icon of this <see cref="DataType"/></returns>
        public Kistl.App.GUI.Icon Icon
        {
            get
            {
                if (_type != null)
                    return _type.DefaultIcon;
                else
                    return null;
            }
        }

        public override string Name
        {
            get
            {
                return _type.Name;
            }
        }

        public override string ToString()
        {
            return Name;
        }

        public virtual IQueryable GetTypedQuery<T>() where T : class, IDataObject
        {
            return DataContext.GetQuery<T>();
        }

        protected virtual IQueryable GetQuery()
        {
            MethodInfo mi = this.GetType().FindGenericMethod("GetTypedQuery", new Type[] { InterfaceType.Type }, null);
            // See Case 552
            var result = (IQueryable)mi.Invoke(this, new object[] { });

            if (_filter != null)
            {
                // attach change events
                foreach (var uiFilter in _filter.OfType<IUIFilterExpression>())
                {
                    uiFilter.FilterChanged += new EventHandler(delegate(object sender, EventArgs e) { ReloadInstances(); });
                }

                foreach (var f in _filter.Where(f => f.Enabled))
                {
                    result = result.Where(f.Predicate, f.FilterValues);
                }
            }

            return result;
        }


        /// <summary>
        /// Loads the instances of this DataType and adds them to the Instances collection
        /// </summary>
        private void LoadInstances()
        {
            foreach (var obj in GetQuery().Cast<IDataObject>().ToList().OrderBy(obj => obj.ToString()))
            {
                _instances.Add(ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(DataContext, obj));
            }
            OnInstancesChanged();
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
            if (InstancesSearchString.Length == 0)
            {
                _instancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(this.Instances);
            }
            else
            {
                // poor man's full text search
                _instancesFiltered = new ReadOnlyObservableCollection<DataObjectModel>(
                    new ObservableCollection<DataObjectModel>(
                        this.Instances.Where(
                            o => o.Name.ToLowerInvariant().Contains(this.InstancesSearchString.ToLowerInvariant())
                            || o.ID.ToString().Contains(this.InstancesSearchString))));
            }
            OnPropertyChanged("InstancesFiltered");
        }

        #endregion

        private DataType _type;
    }
}
