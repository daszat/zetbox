
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
        public new delegate ControlKindViewModel Factory(IKistlContext dataCtx, ControlKind obj);

        public ControlKindViewModel(IViewModelDependencies dependencies, IKistlContext dataCtx, ControlKind obj)
            : base(dependencies, dataCtx, obj)
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
