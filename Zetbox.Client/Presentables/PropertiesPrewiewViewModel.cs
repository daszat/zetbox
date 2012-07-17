namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Models;
    using Zetbox.Client.Presentables.ValueViewModels;

    [ViewModelDescriptor]
    public class PropertiesPrewiewViewModel : ViewModel
    {
        public new delegate PropertiesPrewiewViewModel Factory(IZetboxContext dataCtx, ViewModel parent, DataType dt);

        public PropertiesPrewiewViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, DataType dt)
            : base(appCtx, dataCtx, parent)
        {
            if (dt == null) throw new ArgumentNullException("dt");
            _dataType = dt;
            _dataType.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_dataType_PropertyChanged);
        }

        void _dataType_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Properties":
                case "Methods":
                case "ShowIconInLists":
                case "ShowIdInLists":
                case "ShowNameInLists":
                    Reset();
                    break;
            }
        }

        private DataType _dataType;

        public override string Name
        {
            get { return "PropertiesPrewiewViewModel"; }
        }

        public void Reset()
        {
            ViewModelFactory.CreateDelayedTask(this, () =>
            {
                _displayedColumns = null;
                OnPropertyChanged("DisplayedColumns");
            })
            .Trigger();
        }

        private GridDisplayConfiguration _displayedColumns = null;
        public GridDisplayConfiguration DisplayedColumns
        {
            get
            {
                if (_displayedColumns == null)
                {
                    _displayedColumns = CreateDisplayedColumns();
                }
                return _displayedColumns;
            }
        }

        protected virtual GridDisplayConfiguration CreateDisplayedColumns()
        {
            var result = new GridDisplayConfiguration();
            result.BuildColumns(_dataType, GridDisplayConfiguration.Mode.Editable, true);

            // Fix column control kinds
            var editKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_EnumerationSelectorKind.Find(FrozenContext);
            foreach (var col in result.Columns)
            {
                col.ControlKind = editKind;
                col.GridPreEditKind = editKind;
            }

            return result;
        }

        private IEnumerable<object> _previewInstances;
        public IEnumerable<object> PreviewInstances
        {
            get
            {
                if (_previewInstances == null)
                {
                    _previewInstances = new[] { ViewModelFactory.CreateViewModel<PreviewInstance.Factory>().Invoke(DataContext, this, _dataType) };
                }
                return _previewInstances;
            }
        }

        public class PreviewInstance : ViewModel
        {
            public new delegate PreviewInstance Factory(IZetboxContext dataCtx, PropertiesPrewiewViewModel parent, DataType dt);

            public PreviewInstance(IViewModelDependencies dependencies, IZetboxContext dataCtx, PropertiesPrewiewViewModel parent, DataType dt)
                : base(dependencies, dataCtx, parent)
            {
                _dataType = dt;
                _parent = parent;
            }

            private PropertiesPrewiewViewModel _parent;
            private DataType _dataType;

            public override string Name
            {
                get { return "PreviewInstance"; }
            }

            public class PropPreviewDict
            {
                public PropPreviewDict(DataType dt, IViewModelFactory factory, PropertiesPrewiewViewModel parent)
                {
                    _dataType = dt;
                    _factory = factory;
                    _parent = parent;
                }
                private DataType _dataType;
                private IViewModelFactory _factory;
                private PropertiesPrewiewViewModel _parent;

                private Dictionary<string, ViewModel> _cache = new Dictionary<string, ViewModel>();
                public ViewModel this[string key]
                {
                    get
                    {
                        if (!_cache.ContainsKey(key))
                        {
                            Property prop = _dataType.GetAllProperties().SingleOrDefault(p => p.Name == key);
                            var objVmdl = DataObjectViewModel.Fetch(_factory, _dataType.Context, null, prop);
                            var result = objVmdl.PropertyModelsByName["RequestedWidth"];
                            prop.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(prop_PropertyChanged);
                            _cache.Add(key, result);
                        }
                        return _cache[key];
                    }
                }

                void prop_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
                {
                    switch(e.PropertyName)
                    {
                        case "RequestedWidth":
                        case "DateTimeStyle":
                            _parent.Reset();
                            break;
                    }
                }
            }

            private PropPreviewDict _propertyModelsByName;
            public PropPreviewDict PropertyModelsByName
            {
                get
                {
                    if (_propertyModelsByName == null)
                    {
                        _propertyModelsByName = new PropPreviewDict(_dataType, ViewModelFactory, _parent);
                    }
                    return _propertyModelsByName;
                }
            }
        }
    }
}
