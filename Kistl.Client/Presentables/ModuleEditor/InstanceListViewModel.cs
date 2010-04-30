using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using System.Collections.ObjectModel;
using Kistl.App.Base;
using Kistl.API.Client;
using Kistl.App.GUI;
using ObjectEditorWorkspace = Kistl.Client.Presentables.ObjectEditor.WorkspaceViewModel;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public abstract class InstanceListViewModel : ViewModel
    {
        public new delegate InstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        protected readonly Func<IKistlContext> ctxFactory;

        public InstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx)
        {
            this.module = module;
            this.ctxFactory = ctxFactory;
        }

        private ObservableCollection<DataObjectModel> _instances = null;
        public ObservableCollection<DataObjectModel> Instances
        {
            get
            {
                if (_instances == null)
                {
                    _instances = new ObservableCollection<DataObjectModel>();
                    LoadInstances();
                }
                return _instances;
            }
        }

        protected abstract IQueryable<IDataObject> GetQuery();
        protected void LoadInstances()
        {
            foreach (var obj in GetQuery().ToList().OrderBy(obj => obj.ToString()))
            {
                _instances.Add(ModelFactory.CreateViewModel<DataObjectModel.Factory>(obj).Invoke(DataContext, obj));
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
                    OnPropertyChanged("InstancesSearchString");
                    ExecuteFilter();
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

        public override string ToString()
        {
            return this.Name;
        }

        public abstract InterfaceType InterfaceType { get; }

        protected Module module;

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
    }

    #region *InstanceListViewModel
    public class ObjectClassInstanceListViewModel : InstanceListViewModel
    {
        public new delegate ObjectClassInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public ObjectClassInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            var result = DataContext.GetQuery<ObjectClass>().Where(i => i.Module == module).ToList().AsQueryable();
            if (OnlyBaseClasses)
            {
                result = result.Where(i => i.BaseObjectClass == null);
            }
            return result.Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Object Classes";
            }
        }

        private bool _OnlyBaseClasses = false;
        public bool OnlyBaseClasses
        {
            get
            {
                return _OnlyBaseClasses;
            }
            set
            {
                if (value != _OnlyBaseClasses)
                {
                    _OnlyBaseClasses = value;
                    OnPropertyChanged("OnlyBaseClasses");
                    ReloadInstances();
                }
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(ObjectClass)); }
        }
    }

    public class InterfaceInstanceListViewModel : InstanceListViewModel
    {
        public new delegate InterfaceInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public InterfaceInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<Interface>().Where(i => i.Module == module).ToList().AsQueryable().Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Interfaces";
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(Interface)); }
        }
    }

    public class EnumerationInstanceListViewModel : InstanceListViewModel
    {
        public new delegate EnumerationInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public EnumerationInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }
        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<Enumeration>().Where(i => i.Module == module).ToList().AsQueryable().Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Enumerations";
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(Enumeration)); }
        }
    }

    public class CompoundObjectInstanceListViewModel : InstanceListViewModel
    {
        public new delegate CompoundObjectInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public CompoundObjectInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<CompoundObject>().Where(i => i.Module == module).ToList().AsQueryable().Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Compound Objects";
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(CompoundObject)); }
        }
    }

    public class AssemblyInstanceListViewModel : InstanceListViewModel
    {
        public new delegate AssemblyInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public AssemblyInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<Assembly>().Where(i => i.Module == module).ToList().AsQueryable().Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Assemblies";
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(Assembly)); }
        }
    }

    public class ViewDescriptorInstanceListViewModel : InstanceListViewModel
    {
        public new delegate ViewDescriptorInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public ViewDescriptorInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<ViewDescriptor>().Where(i => i.Module == module).ToList().AsQueryable().Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "View Descriptors";
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(ViewDescriptor)); }
        }
    }

    public class ViewModelDescriptorInstanceListViewModel : InstanceListViewModel
    {
        public new delegate ViewModelDescriptorInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public ViewModelDescriptorInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<ViewModelDescriptor>().Where(i => i.Module == module).ToList().AsQueryable().Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "ViewModel Descriptors";
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(ViewModelDescriptor)); }
        }
    }

    public class RelationInstanceListViewModel : InstanceListViewModel
    {
        public new delegate RelationInstanceListViewModel Factory(IKistlContext dataCtx, Module module);

        public RelationInstanceListViewModel(
            IViewModelDependencies appCtx,
            IKistlContext dataCtx,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<Relation>().Where(i => i.Module == module).ToList().AsQueryable().Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Relations";
            }
        }

        public override InterfaceType InterfaceType
        {
            get { return DataContext.GetInterfaceType(typeof(Relation)); }
        }
    }
    #endregion
}
