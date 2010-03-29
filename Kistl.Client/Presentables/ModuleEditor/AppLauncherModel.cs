namespace Kistl.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using System.Collections.ObjectModel;
    using Kistl.App.Base;
    using Kistl.API.Client;

    public class AppLauncherModel : PresentableModel
    {
        public AppLauncherModel(IGuiApplicationContext appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
        {

        }

        public ObservableCollection<AppLauncherModuleModel> Modules
        {
            get
            {
                if (_modulesCache == null)
                {
                    _modulesCache = new ObservableCollection<AppLauncherModuleModel>();
                    LoadModules();
                }
                return _modulesCache;
            }
        }
        private ObservableCollection<AppLauncherModuleModel> _modulesCache;

        private void LoadModules()
        {
            var modules = DataContext.GetQuery<Module>().ToList();
            foreach (var m in modules)
            {
                Modules.Add((AppLauncherModuleModel)Factory.CreateSpecificModel<AppLauncherModuleModel>(DataContext, m));
            }
        }

        public override string Name
        {
            get { return "Module Editor AppLauncher"; }
        }
    }

    public class AppLauncherModuleModel
        : DataObjectModel
    {
        public AppLauncherModuleModel(IGuiApplicationContext appCtx, IKistlContext dataCtx, Module m)
            : base(appCtx, dataCtx, m)
        {

        }

        public override string Name
        {
            get { return this.Object.ToString(); }
        }

        public void ShowWorkspace()
        {
            var factory = AppContext.Factory;
            var newWorkspace = factory.CreateSpecificModel<WorkspaceModel>(KistlContext.GetContext(), this.Object);
            // This is another way...
            // newWorkspace.ShowForeignModel(this);
            factory.ShowModel(newWorkspace, true);
        }
    }
}
