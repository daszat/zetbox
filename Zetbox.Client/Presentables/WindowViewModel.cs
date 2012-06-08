
namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    /// <summary>
    /// A top-level working space. This is the user's "Unit of Work".
    /// </summary>
    public abstract class WindowViewModel : ViewModel
    {
        public new delegate WindowViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public WindowViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
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
