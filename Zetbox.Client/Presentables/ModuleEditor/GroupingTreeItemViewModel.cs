namespace Zetbox.Client.Presentables.ModuleEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class GroupingTreeItemViewModel : ViewModel
    {
        public new delegate GroupingTreeItemViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name);

        public GroupingTreeItemViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, string name)
            : base(appCtx, dataCtx, parent)
        {
            _name = name;
        }

        private string _name;
        public override string Name
        {
            get { return _name; }
        }

        private List<ViewModel> _children = new List<ViewModel>();
        public IList<ViewModel> Children
        {
            get
            {
                return _children;
            }
        }

        public ViewModel DashboardViewModel
        {
            get
            {
                return null;
            }
        }
    }
}
