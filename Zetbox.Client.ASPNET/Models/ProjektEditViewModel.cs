namespace Zetbox.Client.ASPNET.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;

    // No descriptor
    public class ProjektEditViewModel : ViewModel
    {
        public new delegate ProjektEditViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public ProjektEditViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "ProjektEditViewModel"; }
        }

        public int ID { get; set; }

        private DataObjectViewModel _object;
        public DataObjectViewModel Object
        {
            get
            {
                if (_object == null)
                {
                    _object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, DataContext.Find<Zetbox.App.Projekte.Projekt>(ID));
                }
                return _object;
            }
        }
    }
}
