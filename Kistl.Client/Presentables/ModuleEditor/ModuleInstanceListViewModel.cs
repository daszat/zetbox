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
using Kistl.API.Configuration;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public abstract class ModuleInstanceListViewModel : InstanceListViewModel
    {
        public new delegate ModuleInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public ModuleInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, ctxFactory)
        {
            this.module = module;
        }

        protected Module module;
    }

    #region *InstanceListViewModel
    public class ObjectClassInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate ObjectClassInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public ObjectClassInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            // TODO: Add support for multiple filter. Similar to case 1344
            var result = DataContext.GetQuery<ObjectClass>().Where(i => i.Module == module).ToList();
            if (OnlyBaseClasses)
            {
                result = result.AsQueryable().Where(i => i.BaseObjectClass == null).ToList();
            }
            return result.AsQueryable().Cast<IDataObject>();
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
    }

    public class InterfaceInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate InterfaceInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public InterfaceInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
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
    }

    public class EnumerationInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate EnumerationInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public EnumerationInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
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
    }

    public class CompoundObjectInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate CompoundObjectInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public CompoundObjectInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
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
    }

    public class AssemblyInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate AssemblyInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public AssemblyInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
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
    }

    public class ViewDescriptorInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate ViewDescriptorInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public ViewDescriptorInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
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
    }

    public class ViewModelDescriptorInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate ViewModelDescriptorInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public ViewModelDescriptorInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
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
    }

    public class RelationInstanceListViewModel : ModuleInstanceListViewModel
    {
        public new delegate RelationInstanceListViewModel Factory(IKistlContext dataCtx, DataType type, Module module);

        public RelationInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Module module,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, module, ctxFactory)
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
    }
    #endregion
}
