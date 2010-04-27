using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client.Presentables.KistlBase
{
    public class ApplicationViewModel : ViewModel
    {
        public new delegate ApplicationViewModel Factory(IKistlContext dataCtx, string name, Type wndMdlType);

        public ApplicationViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx,
            string name, Type wndMdlType)
            : base(appCtx, dataCtx)
        {
            _name = name;
            _wndMdlType = wndMdlType;
        }

        private Type _wndMdlType;
        public Type WindowModelType
        {
            get
            {
                return _wndMdlType;
            }
        }

        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        #region Open Applicaton

        private static OpenApplicatonCommand _openApplicatonCommand = null;
        public ICommand OpenApplicatonCommand
        {
            get
            {
                if (_openApplicatonCommand == null)
                {
                    _openApplicatonCommand = ModelFactory.CreateViewModel<OpenApplicatonCommand.Factory>().Invoke(DataContext);
                }
                return _openApplicatonCommand;
            }
        }

        #endregion
    }


    internal class OpenApplicatonCommand : CommandModel
    {
        public new delegate OpenApplicatonCommand Factory(IKistlContext dataCtx);

        private readonly Func<IKistlContext> ctxFactory;

        public OpenApplicatonCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, Func<IKistlContext> ctxFactory)
            : base(appCtx, dataCtx, "Open Application", "Opens an Application in a new window")
        {
            this.ctxFactory = ctxFactory;
        }

        public override bool CanExecute(object data)
        {
            return data != null
                && data is ApplicationViewModel;
        }

        protected override void DoExecute(object data)
        {
            if (CanExecute(data))
            {
                var externalCtx = ctxFactory();
                var appMdl = data as ApplicationViewModel;

                // responsibility to externalCtx's disposal passes to newWorkspace
                var newWorkspace = ModelFactory.CreateViewModel<WindowViewModel.Factory>(appMdl.WindowModelType).Invoke(externalCtx);
                ModelFactory.ShowModel(newWorkspace, true);
            }
        }
    }
}
