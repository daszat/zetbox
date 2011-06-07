
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class WindowViewModel : ViewModel
    {
        public new delegate WindowViewModel Factory(IKistlContext dataCtx, ViewModel parent);

        public WindowViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public WindowViewModel(bool designMode)
            : base(designMode)
        {
        }

        private bool _show = true;
        public bool Show
        {
            get
            {
                return _show;
            }
            set
            {
                if (!value && !CanClose()) return;
                _show = value;
                OnPropertyChanged("Show");
            }
        }

        /// <summary>
        /// Views should call this before closing
        /// </summary>
        /// <returns></returns>
        public virtual bool CanClose()
        {
            return true;
        }
    }
}
