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

    public interface ISearchViewModel
    {
        string Name { get; }

        int PageSize { get; }
        int Page { get; }
        int Count { get; }
        bool ShowNextPage { get; }
        int NextPage { get; }
        bool ShowPrevPage { get; }
        int PrevPage { get; }
        string CountAsText { get; }
    }

    /// <summary>
    /// The only purpose for this kind of ViewModel is to support ASP.NET MVC to recreated it's state while searching for objects.
    /// As a result the well known DataObjectViewModel are passed back.
    /// </summary>
    /// <remarks>
    /// Currently each derived class has to implement it's own search fields and override ApplyFilter()
    /// </remarks>
    public class SearchViewModel<TModel> : GenericSearchViewModel<TModel, DataObjectViewModel>
        where TModel : class, IDataObject
    {
        public new delegate SearchViewModel<TModel> Factory(IZetboxContext dataCtx, ViewModel parent);

        public SearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }
    }

    /// <summary>
    /// The only purpose for this kind of ViewModel is to support ASP.NET MVC to recreated it's state while searching for objects.
    /// As a result the well known DataObjectViewModel are passed back.
    /// </summary>
    /// <remarks>
    /// Currently each derived class has to implement it's own search fields and override ApplyFilter()
    /// </remarks>
    public class GenericSearchViewModel<TModel, TViewModel> : ViewModel, ISearchViewModel
        where TModel : class, IDataObject
        where TViewModel : DataObjectViewModel
    {
        public new delegate GenericSearchViewModel<TModel, TViewModel> Factory(IZetboxContext dataCtx, ViewModel parent);

        public GenericSearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
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

        protected virtual IQueryable<TModel> ApplyOrderBy(IQueryable<TModel> qry)
        {
            return qry;
        }

        protected virtual IQueryable<TModel> ApplyPaging(IQueryable<TModel> qry)
        {
            return qry.Skip((Page - 1) * PageSize).Take(PageSize);
        }

        protected virtual TViewModel FetchResultViewModel(TModel obj)
        {
            return (TViewModel)DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, obj);
        }

        private List<TViewModel> _result;
        public IEnumerable<TViewModel> Result
        {
            get
            {
                if (_result == null)
                {
                    var qry = DataContext.GetQuery<TModel>();

                    qry = ApplyFilter(qry);
                    qry = ApplyOrderBy(qry);
                    qry = ApplyPaging(qry);

                    _result = qry
                                .ToList() // Send query to server
                                .Select(FetchResultViewModel)
                                .ToList(); // save the result
                }
                return _result;
            }
        }

        public void InvalidateResult()
        {
            _result = null;
        }

        #region Paging
        private int? _pageSize;
        public virtual int PageSize
        {
            get
            {
                return _pageSize ?? GetDefaultPageSize();
            }
            set
            {
                _pageSize = value;
            }
        }

        protected virtual int GetDefaultPageSize()
        {
            return 25;
        }

        private int _page = 1;
        /// <summary>
        /// Current Page
        /// </summary>
        public int Page
        {
            get
            {
                if (IsNavigating && _page != NavigateTo)
                {
                    // Respect users wish
                    _page = NavigateTo;
                }
                return _page;
            }
            set
            {
                _page = value;
            }
        }

        /// <summary>
        /// Page to navigate to, used in post-back scenario
        /// </summary>
        public int NavigateTo { get; set; }
        public bool IsNavigating
        {
            get
            {
                return NavigateTo != default(int);
            }
        }

        public int Count
        {
            get
            {
                return Result.Count();
            }
        }

        public bool ShowNextPage
        {
            get
            {
                return Count >= PageSize;
            }
        }
        public int NextPage
        {
            get
            {
                return ShowNextPage ? Page + 1 : Page;
            }
        }

        public bool ShowPrevPage
        {
            get
            {
                return Page > 1;
            }
        }
        public int PrevPage
        {
            get
            {
                return ShowPrevPage ? Page - 1 : 0;
            }
        }

        /// <summary>
        /// Format string for the <see cref="CountAsText" /> property. {0} = Count
        /// </summary>
        public string CountAsTextFormatString { get; set; }

        /// <summary>
        /// Uses <see cref="CountAsTextFormatString" /> as format string or the default format string "Items"
        /// </summary>
        public string CountAsText
        {
            get
            {
                string countStr = string.Empty;

                if (Count > 0 && (ShowNextPage || Page != 1))
                {
                    var from = (Page - 1) * PageSize;
                    countStr = string.Format("{0} - {1}", from + 1, from + Count);
                }
                else
                {
                    countStr = Count.ToString();
                }

                if (!string.IsNullOrEmpty(CountAsTextFormatString))
                {
                    return string.Format(CountAsTextFormatString, countStr);
                }
                else
                {
                    return string.Format(ASPNETToolkitResources.CountAsTextFormatString, countStr);
                }
            }
        }
        #endregion
    }
}
