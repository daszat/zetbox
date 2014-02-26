namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Models;
    using Zetbox.App.Base;
    using Zetbox.Client.Presentables.ValueViewModels;
    using System.Web.Mvc;
    using System.ComponentModel;

    /// <summary>
    /// The only purpose for this kind of ViewModel is to support ASP.NET MVC to recreated it's state while searching for objects.
    /// As a result the well known DataObjectViewModel are passed back.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class SearchViewModel<TModel> : ViewModel
        where TModel : class, IDataObject
    {
        public new delegate SearchViewModel<TModel> Factory(IZetboxContext dataCtx, ViewModel parent);

        public SearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public override string Name
        {
            get { return "SearchViewModel"; }
        }

        protected virtual IQueryable<TModel> ApplyFilter(IQueryable<TModel> qry)
        {
            return qry;
        }

        public IEnumerable<DataObjectViewModel> Result
        {
            get
            {
                var qry = DataContext.GetQuery<TModel>();
                qry = ApplyFilter(qry);

                return qry.ToList().Select(p => DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, p));
            }
        }
    }
}
