using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;

namespace Kistl.Client.Presentables.ModuleEditor
{
    public class WorkspaceModel : PresentableModel
    {
        public WorkspaceModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {
            CurrentModule = dataCtx.GetQuery<Module>().FirstOrDefault();
        }

        public WorkspaceModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, int moduleID)
            : base(appCtx, dataCtx)
        {
            CurrentModule = dataCtx.Find<Module>(moduleID);
        }

        public Module CurrentModule { get; set; }

        public override string Name
        {
            get { return "Module Editor Workspace"; }
        }

        private ReadOnlyObservableCollection<PresentableModel> _TreeItems = null;
        public ReadOnlyObservableCollection<PresentableModel> TreeItems
        {
            get
            {
                if (_TreeItems == null)
                {
                    var lst = new ObservableCollection<PresentableModel>();
                    // ObjectClass
                    lst.Add(new ObjectClassDashboardModel(AppContext, DataContext, CurrentModule, DataContext.FindPersistenceObject<ObjectClass>(new Guid("20888DFC-1FBC-47C8-9F3C-C6A30A5C0048"))));
                    // Enumeration
                    lst.Add(new EnumerationDashboardModel(AppContext, DataContext, CurrentModule, DataContext.FindPersistenceObject<ObjectClass>(new Guid("EE475DE2-D626-49E9-9E40-6BB12CB026D4"))));
                    // CompoundObject
                    lst.Add(new CompoundObjectDashboardModel(AppContext, DataContext, CurrentModule, DataContext.FindPersistenceObject<ObjectClass>(new Guid("2CB3F778-DD6A-46C7-AD2B-5F8691313035"))));
                    // Assemblies
                    lst.Add(new AssemblyDashboardModel(AppContext, DataContext, CurrentModule, DataContext.FindPersistenceObject<ObjectClass>(new Guid("A590A975-66E5-421C-AA97-7AB3169E0E9B"))));

                    _TreeItems = new ReadOnlyObservableCollection<PresentableModel>(lst);
                }
                return _TreeItems;
            }
        }

        private PresentableModel _selectedItem;
        public PresentableModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if (_selectedItem != value)
                {
                    _selectedItem = value;
                    OnPropertyChanged("SelectedItem");
                }
            }
        }
    }

    public abstract class ModuleEditorDashboardModel : DataTypeModel
    {
        public ModuleEditorDashboardModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            Module module,
            ObjectClass cls)
            : base(appCtx, dataCtx, cls)
        {
            this.module = module;
        }

        protected abstract IQueryable<IDataObject> GetQuery();

        protected override void QueryHasInstances()
        {
            var obj = GetQuery().FirstOrDefault();
            HasInstances = (obj != null);
        }

        protected override void LoadInstances()
        {
            foreach (var obj in GetQuery().ToList().OrderBy(obj => obj.ToString()))
            {
                Instances.Add((DataObjectModel)Factory.CreateDefaultModel(DataContext, obj));
            }
        }

        public override string ToString()
        {
            return this.Name;
        }

        protected Module module;
    }

    public class ObjectClassDashboardModel : ModuleEditorDashboardModel
    {
        public ObjectClassDashboardModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            Module module,
            ObjectClass cls)
            : base(appCtx, dataCtx, module, cls)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<ObjectClass>().Where(i => i.Module == module).Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Object Classes";
            }
        }
    }

    public class EnumerationDashboardModel : ModuleEditorDashboardModel
    {
        public EnumerationDashboardModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            Module module,
            ObjectClass cls)
            : base(appCtx, dataCtx, module, cls)
        {
        }
        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<Enumeration>().Where(i => i.Module == module).Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Enumerations";
            }
        }
    }

    public class CompoundObjectDashboardModel : ModuleEditorDashboardModel
    {
        public CompoundObjectDashboardModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            Module module,
            ObjectClass cls)
            : base(appCtx, dataCtx, module, cls)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<CompoundObject>().Where(i => i.Module == module).Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Compound Objects";
            }
        }
    }

    public class AssemblyDashboardModel : ModuleEditorDashboardModel
    {
        public AssemblyDashboardModel(
            IGuiApplicationContext appCtx,
            IKistlContext dataCtx,
            Module module,
            ObjectClass cls)
            : base(appCtx, dataCtx, module, cls)
        {
        }

        protected override IQueryable<IDataObject> GetQuery()
        {
            return DataContext.GetQuery<Assembly>().Where(i => i.Module == module).Cast<IDataObject>();
        }

        public override string Name
        {
            get
            {
                return "Assemblies";
            }
        }
    }
}
