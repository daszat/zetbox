namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;

    // No descriptor
    public class DataObjectEditViewModel<TModel> : ViewModel
        where TModel : class, IDataObject
    {
        public new delegate DataObjectEditViewModel<TModel> Factory(IZetboxContext dataCtx, ViewModel parent);

        public DataObjectEditViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "DataObjectEditViewModel"; }
        }

        public int ID { get; set; }

        private DataObjectViewModel _object;
        public DataObjectViewModel Object
        {
            get
            {
                if (_object == null)
                {
                    _object = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, DataContext.Find<TModel>(ID));
                }
                return _object;
            }
        }
    }
}
