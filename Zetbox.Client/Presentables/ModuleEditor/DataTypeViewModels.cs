
namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.Client.Presentables.ZetboxBase;
    using ObjectEditorWorkspace = Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel;

    public class ModuleGraphViewModel : Presentables.DataObjectViewModel
    { 
        public new delegate ModuleGraphViewModel Factory(IZetboxContext dataCtx, DiagramViewModel parent, Module obj);

        private DiagramViewModel _diagMdl;
        protected readonly Func<IZetboxContext> ctxFactory;

        public ModuleGraphViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, DiagramViewModel parent,
            Module obj, Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent, obj)
        {
            this._diagMdl = parent;
            this.ctxFactory = ctxFactory;
            this.Module = obj;
            this._diagMdl.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_diagMdl_PropertyChanged);
        }

        void _diagMdl_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case "FilterValue":
                    OnPropertyChanged("DataTypes");
                    break;
            }
        }

        public override string Name
        {
            get
            {
                return Module.Name;
            }
        }

        public Module Module { get; private set; }

        private List<DataTypeGraphModel> _dataTypeViewModels = null;
        public IEnumerable<DataTypeGraphModel> DataTypes
        {
            get
            {
                if (_dataTypeViewModels == null)
                {
                    _dataTypeViewModels = DataContext.GetQuery<DataType>()
                                            .Where(dt => dt.Module.ExportGuid == Module.ExportGuid)
                                            .ToList()
                                            .Select(i => ViewModelFactory.CreateViewModel<DataTypeGraphModel.Factory>().Invoke(DataContext, _diagMdl, i))
                                            .ToList();
                }
                if (!string.IsNullOrEmpty(_diagMdl.FilterValue))
                {
                    var str = _diagMdl.FilterValue.ToLowerInvariant();
                    return _dataTypeViewModels.Where(i => i.Name.ToLowerInvariant().Contains(str));
                }
                else
                {
                    return _dataTypeViewModels;
                }
            }
        }

        public void Refresh()
        {
            if (_dataTypeViewModels != null)
            {
                var newDataTypes = DataContext.GetQuery<DataType>().Where(dt => dt.Module.ExportGuid == Module.ExportGuid).ToList();
                // Add new ones, keep existing ones
                _dataTypeViewModels.AddRange(newDataTypes.Except(_dataTypeViewModels.Select(i => i.DataType)).Select(i => ViewModelFactory.CreateViewModel<DataTypeGraphModel.Factory>().Invoke(DataContext, _diagMdl, i)));
                _dataTypeViewModels.RemoveAll(dt => !newDataTypes.Contains(dt.DataType));
                _dataTypeViewModels.Sort((a, b) => a.Name.CompareTo(b.Name));

                // create a new instance so that WPF will realy refresh the list
                _dataTypeViewModels = new List<DataTypeGraphModel>(_dataTypeViewModels);
                OnPropertyChanged("DataTypes");
            }
        }

        private bool? _isExpanded = null;
        public bool IsExpanded
        {
            get
            {
                if (_isExpanded == null)
                {
                    _isExpanded = _diagMdl.Module.ExportGuid == this.Module.ExportGuid;
                }
                return _isExpanded.Value;
            }
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged("IsExpanded");
                }
            }
        }
    }

    public class DataTypeGraphModel : Presentables.DataTypeViewModel, IOpenCommandParameter
    {
        public new delegate DataTypeGraphModel Factory(IZetboxContext dataCtx, DiagramViewModel parent, DataType obj);

        private DiagramViewModel _diagMdl;
        protected readonly Func<IZetboxContext> ctxFactory;

        public DataTypeGraphModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, DiagramViewModel parent,
            DataType obj, Func<IZetboxContext> ctxFactory)
            : base(appCtx, dataCtx, parent, obj)
        {
            this._diagMdl = parent;
            this.ctxFactory = ctxFactory;
            this.DataType = obj;
        }

        public override string Name
        {
            get
            {
                return DataType.Name;
            }
        }

        public override System.Drawing.Image Icon
        {
            get
            {
                return base.Icon;
            }
        }

        public DataType DataType { get; private set; }

        private bool _isChecked = false;
        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }
            set
            {
                SetChecked(value, true);
            }
        }

        internal void SetChecked(bool chk, bool recreateGraph)
        {
            if (_isChecked != chk)
            {
                _isChecked = chk;
                if (recreateGraph) _diagMdl.RecreateGraph();
                OnPropertyChanged("IsChecked");
            }
        }

        private bool _isGraphChecked = false;
        public bool IsGraphChecked
        {
            get
            {
                return _isGraphChecked;
            }
            set
            {
                if (_isGraphChecked != value)
                {
                    _isGraphChecked = value;
                    OnPropertyChanged("IsGraphChecked");
                }
            }
        }

        private OpenDataObjectCommand _open = null;
        public ICommandViewModel Open
        {
            get
            {
                if (_open == null)
                {
                    _open = ViewModelFactory.CreateViewModel<OpenDataObjectCommand.Factory>().Invoke(DataContext, this);
                }

                return _open;
            }
        }

        private ReadOnlyProjectedList<Property, DescribedPropertyViewModel> _propertyModels;
        public IReadOnlyList<DescribedPropertyViewModel> DescribedPropertyModels
        {
            get
            {
                if (_propertyModels == null)
                {
                    _propertyModels = new ReadOnlyProjectedList<Property, DescribedPropertyViewModel>(
                        DataType.Properties.OrderBy(p => p.Name).ToList(),
                        property => ViewModelFactory.CreateViewModel<DescribedPropertyViewModel.Factory>().Invoke(DataContext, this, property),
                        m => m.DescribedProperty);
                }
                return _propertyModels;
            }
        }

        private ReadOnlyProjectedList<Method, DescribedMethodViewModel> _methodModels;
        public IReadOnlyList<DescribedMethodViewModel> DescribedMethods
        {
            get
            {
                if (_methodModels == null)
                {
                    _methodModels = new ReadOnlyProjectedList<Method, DescribedMethodViewModel>(
                        DataType.Methods.OrderBy(m => m.Name).ToList(),
                        m => ViewModelFactory.CreateViewModel<DescribedMethodViewModel.Factory>().Invoke(DataContext, this, m),
                        m => m.DescribedMethod);
                }
                return _methodModels;
            }
        }

        public IEnumerable<DescribedMethodViewModel> DescribedCustomMethods
        {
            get
            {
                return DescribedMethods.Where(m => !m.IsDefaultMethod);
            }
        }

        #region IOpenCommandParameter members
        bool IOpenCommandParameter.AllowOpen { get { return true; } }
        IEnumerable<ViewModel> ICommandParameter.SelectedItems { get { return new[] { this }; } }
        #endregion
    }

    public class DescribedMethodViewModel
        : DataObjectViewModel
    {
        public new delegate DescribedMethodViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Method meth);

        public DescribedMethodViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Method meth)
            : base(appCtx, dataCtx, parent, meth)
        {
            _method = meth;
        }
        private Method _method;
        public Method DescribedMethod { get { return _method; } }

        public override string Name
        {
            get
            {
                return _method.Name;
            }
        }

        public bool IsDefaultMethod
        {
            get
            {
                return _method.IsDefaultMethod();
            }
        }

        public string ReturnTypeString
        {
            get
            {
                var p = _method.GetReturnParameter();
                if (p == null) return "void";
                return p.GetParameterTypeString();
            }
        }

        public string ShortReturnTypeString
        {
            get
            {
                var p = _method.GetReturnParameter();
                if (p == null) return "void";

                if (p is BoolParameter)
                {
                    return "bool";
                }
                else if (p is IntParameter)
                {
                    return "int";
                }
                else if (p is DoubleParameter)
                {
                    return "double";
                }
                else if (p is StringParameter)
                {
                    return "string";
                }
                /*else if (p is GuidParameter)
                {
                    return "Guid";
                }*/
                else if (p is DateTimeParameter)
                {
                    return "DateTime";
                }
                else
                {
                    return ReturnTypeString;
                }
            }
        }
    }

    public class DescribedPropertyViewModel
        : DataObjectViewModel
    {
        public new delegate DescribedPropertyViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Property prop);

        public DescribedPropertyViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            Property prop)
            : base(appCtx, dataCtx, parent, prop)
        {
            _prop = prop;
        }
        private Property _prop;

        public Property DescribedProperty { get { return _prop; } }

        public override string Name
        {
            get
            {
                return _prop.Name;
            }
        }

        public string TypeString
        {
            get
            {
                return _prop.GetPropertyTypeString();
            }
        }

        public string ShortTypeString
        {
            get
            {
                if (_prop is BoolProperty)
                {
                    return "bool";
                }
                else if (_prop is IntProperty)
                {
                    return "int";
                }
                else if (_prop is DecimalProperty)
                {
                    return "decimal";
                }
                else if (_prop is DoubleProperty)
                {
                    return "double";
                }
                else if (_prop is StringProperty)
                {
                    return "string";
                }
                else if (_prop is GuidProperty)
                {
                    return "Guid";
                }
                else if (_prop is DateTimeProperty)
                {
                    return "DateTime";
                }
                else
                {
                    return TypeString;
                }
            }
        }

        public bool IsList
        {
            get
            {
                return _prop.GetIsList();
            }
        }
    }
}
