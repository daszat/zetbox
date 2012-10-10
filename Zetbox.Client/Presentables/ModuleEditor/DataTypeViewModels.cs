
namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.API.Utils;
    using Zetbox.App.Extensions;
    using ObjectEditorWorkspace = Zetbox.Client.Presentables.ObjectEditor.WorkspaceViewModel;

    public class DataTypeGraphModel : Presentables.DataTypeViewModel
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
                return string.Format("{0}.{1}", DataType.Module.Name, DataType.Name);
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

        private ICommandViewModel _open = null;
        public ICommandViewModel Open
        {
            get
            {
                if (_open == null)
                {
                    _open = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(
                        DataContext, this, "Open", "Opens the current DataType", () =>
                        {
                            var newWorkspace = ViewModelFactory.CreateViewModel<ObjectEditorWorkspace.Factory>().Invoke(ctxFactory(), null);
                            newWorkspace.ShowForeignModel(this);
                            ViewModelFactory.ShowModel(newWorkspace, true);
                        }, null, null);
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
