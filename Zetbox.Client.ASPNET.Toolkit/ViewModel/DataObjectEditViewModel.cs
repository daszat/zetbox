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
    /// This class uses a DataObjectViewModel as the objects ViewModel type.
    /// </summary>
    /// <remarks>No descriptor, as it's a ASP.NET MCV only view model</remarks>
    /// <typeparam name="TModel"></typeparam>
    public class DataObjectEditViewModel<TModel> : GenericDataObjectEditViewModel<TModel, DataObjectViewModel>
        where TModel : class, IDataObject
    {
        public new delegate DataObjectEditViewModel<TModel> Factory(IZetboxContext dataCtx, ViewModel parent);

        public DataObjectEditViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }
    }

    /// <summary>
    /// The only purpose for this kind of ViewModel is to support ASP.NET MVC to recreated it's state.
    /// Internal you should continue using the aleady defined ViewModels for each object.
    /// </summary>
    /// <remarks>No descriptor, as it's a ASP.NET MCV only view model</remarks>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    public class GenericDataObjectEditViewModel<TModel, TViewModel> : ViewModel
        where TModel : class, IDataObject
        where TViewModel : DataObjectViewModel
    {
        public new delegate GenericDataObjectEditViewModel<TModel, TViewModel> Factory(IZetboxContext dataCtx, ViewModel parent);

        public GenericDataObjectEditViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
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
                    if (ID > Helper.INVALIDID)
                    {
                        _object = FindObject();
                    }
                    else
                    {
                        _object = CreateNewInstance();
                    }
                }
                return _object;
            }
        }

        private TViewModel _viewModel;
        public TViewModel ViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = FetchViewModel();
                }
                return _viewModel;
            }
        }

        protected virtual TModel CreateNewInstance()
        {
            return DataContext.Create<TModel>();
        }

        protected virtual TModel FindObject()
        {
            return DataContext.Find<TModel>(ID);
        }

        protected virtual TViewModel FetchViewModel()
        {
            return (TViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, null, Object);
        }
    }
}
