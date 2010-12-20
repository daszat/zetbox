
namespace Kistl.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.GUI;

    [ViewModelDescriptor]
    public class ControlKindViewModel
        : DataObjectViewModel
    {
#if MONO
        // See https://bugzilla.novell.com/show_bug.cgi?id=660553
        public delegate ControlKindViewModel Factory(IKistlContext dataCtx, ControlKind obj);
#else
        public new delegate ControlKindViewModel Factory(IKistlContext dataCtx, ControlKind obj);
#endif

        public ControlKindViewModel(IViewModelDependencies dependencies, KistlConfig config, IKistlContext dataCtx, ControlKind obj)
            : base(dependencies, config, dataCtx, obj)
        {
            Kind = obj;
        }

        public ControlKind Kind { get; private set; }

        private ReadOnlyObservableCollection<ControlKindViewModel> _childControlKinds = null;
        public ReadOnlyObservableCollection<ControlKindViewModel> ChildControlKinds
        {
            get
            {
                if (_childControlKinds == null)
                {
                    _childControlKinds = new ReadOnlyObservableCollection<ControlKindViewModel>(new ObservableCollection<ControlKindViewModel>(
                        Kind.ChildControlKinds
                            .OrderBy(i => i.Name)
                            .Select(i => ViewModelFactory.CreateViewModel<ControlKindViewModel.Factory>().Invoke(DataContext, i))));
                }
                return _childControlKinds;
            }
        }
    }
}
