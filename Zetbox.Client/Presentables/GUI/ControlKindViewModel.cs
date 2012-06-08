
namespace Zetbox.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class ControlKindViewModel
        : DataObjectViewModel
    {
        public new delegate ControlKindViewModel Factory(IZetboxContext dataCtx, ViewModel parent, ControlKind obj);

        public ControlKindViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, ControlKind obj)
            : base(dependencies, dataCtx, parent, obj)
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
                            .Select(i => ViewModelFactory.CreateViewModel<ControlKindViewModel.Factory>().Invoke(DataContext, this, i))));
                }
                return _childControlKinds;
            }
        }
    }
}
