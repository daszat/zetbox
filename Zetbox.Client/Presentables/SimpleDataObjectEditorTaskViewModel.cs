
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables.ZetboxBase;

    [ViewModelDescriptor]
    public class SimpleDataObjectEditorTaskViewModel
        : WindowViewModel
    {
        public new delegate SimpleDataObjectEditorTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent,
            ViewModel obj);

        public SimpleDataObjectEditorTaskViewModel(
            IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent,
            ViewModel obj)
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
                    _CloseCommand = ViewModelFactory.CreateViewModel<SimpleCommandViewModel.Factory>().Invoke(DataContext, this, SimpleDataObjectEditorTaskViewModelResources.Close, SimpleDataObjectEditorTaskViewModelResources.Close_Tooltip, Close, null, null);
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

        public ViewModel Object
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
