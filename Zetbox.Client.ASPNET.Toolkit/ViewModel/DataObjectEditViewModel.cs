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

        private TModel _object;
        public TModel Object
        {
            get
            {
                if (_object == null)
                {
                    if (ID != default(int))
                    {
                        _object = DataContext.Find<TModel>(ID);
                    }
                    else
                    {
                        _object = DataContext.Create<TModel>();
                    }
                }
                return _object;
            }
        }

        private DataObjectViewModel _viewModel;
        public DataObjectViewModel ViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, Object);
                }
                return _viewModel;
            }
        }
    }
}
