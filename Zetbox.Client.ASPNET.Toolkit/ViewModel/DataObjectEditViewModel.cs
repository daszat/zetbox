namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ValueViewModels;

    /// <summary>
    /// The only purpose for this kind of ViewModel is to support ASP.NET MVC to recreated it's state.
    /// Internal you should continue using the aleady defined ViewModels for each object.
    /// </summary>
    /// <remarks>No descriptor, as it's a ASP.NET MCV only view model</remarks>
    /// <typeparam name="TModel"></typeparam>
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
            get { return ViewModel.Name; }
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
