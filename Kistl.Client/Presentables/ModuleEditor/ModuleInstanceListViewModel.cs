using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
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
    #region ObjectClassInstanceListViewModel
    public class ObjectClassInstanceListViewModel : InstanceListViewModel
    {
        public new delegate ObjectClassInstanceListViewModel Factory(IKistlContext dataCtx, DataType type);

        public ObjectClassInstanceListViewModel(
            IViewModelDependencies appCtx,
            KistlConfig cfg,
            IKistlContext dataCtx,
            DataType type,
            Func<IKistlContext> ctxFactory)
            : base(appCtx, cfg, dataCtx, type, ctxFactory)
        {
        }

        protected override IQueryable GetQuery()
        {
            // TODO: Add support for multiple filter. Similar to case 1344
            var result = base.GetQuery();
            if (OnlyBaseClasses)
            {
                result = result.Where("BaseObjectClass = null");
            }
            return result;
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
    #endregion
}
