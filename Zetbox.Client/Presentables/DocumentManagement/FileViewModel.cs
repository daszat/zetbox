namespace Kistl.Client.Presentables.DocumentManagement
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using at.dasz.DocumentManagement;
    using Kistl.API;
    using Kistl.API.Configuration;

    [ViewModelDescriptor]
    public class FileViewModel : DataObjectViewModel
    {
        public new delegate FileViewModel Factory(IKistlContext dataCtx, ViewModel parent, IDataObject obj);

        public FileViewModel(
            IViewModelDependencies appCtx, KistlConfig config, IKistlContext dataCtx, ViewModel parent,
            File obj)
            : base(appCtx, dataCtx, parent, obj)
        {
            this.File = obj;
        }

        protected override System.Collections.ObjectModel.ObservableCollection<ICommandViewModel> CreateCommands()
        {
            return base.CreateCommands();
        }

        public File File { get; private set; }
    }
}
