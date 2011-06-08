
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.Client.Presentables.KistlBase;

    [ViewModelDescriptor]
    public class SimpleDataObjectEditorTaskViewModel
        : WindowViewModel
    {
        public new delegate SimpleDataObjectEditorTaskViewModel Factory(IKistlContext dataCtx, ViewModel parent,
            DataObjectViewModel obj);

        public SimpleDataObjectEditorTaskViewModel(
            IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent,
            DataObjectViewModel obj)
            : base(appCtx, dataCtx, parent)
        {
            if (obj == null) throw new ArgumentNullException("obj");
            this.Object = obj;
        }

        #region Commands
        private ICommandViewModel _CloseCommand = null;
        public ICommandViewModel CloseCommand
        {
            get
            {
                if (_CloseCommand == null)
                {
                    _CloseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, SimpleDataObjectEditorTaskViewModelResources.Close, SimpleDataObjectEditorTaskViewModelResources.Close_Tooltip, Close, null);
                }
                return _CloseCommand;
            }
        }

        public void Close()
        {
            base.Show = false;
        }
        #endregion


        #region Public interface

        public DataObjectViewModel Object
        {
            get; private set;
        }

        #endregion

        public override string Name
        {
            get { return string.Format(SimpleDataObjectEditorTaskViewModelResources.Name, Object.Name); }
        }
    }
}
