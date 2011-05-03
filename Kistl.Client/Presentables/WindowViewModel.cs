
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class WindowViewModel : ViewModel
    {
        public new delegate WindowViewModel Factory(IKistlContext dataCtx);

        public WindowViewModel(IViewModelDependencies appCtx, IKistlContext dataCtx)
            : base(appCtx, dataCtx)
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

        private bool _isBusy = false;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged("IsBusy");
                }
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
