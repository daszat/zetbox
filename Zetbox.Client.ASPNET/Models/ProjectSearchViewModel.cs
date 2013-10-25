namespace Zetbox.Client.ASPNET.Models
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

    public class ProjectSearchViewModel : SearchViewModel<Zetbox.App.Projekte.Projekt>
    {
        public new delegate ProjectSearchViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public ProjectSearchViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        private StringValueViewModel _ProjectName;
        public StringValueViewModel ProjectName
        {
            get
            {
                if (_ProjectName == null)
                {
                    _ProjectName = ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(DataContext, this, new ClassValueModel<string>("Project name", "", false, false));
                }
                return _ProjectName;
            }
        }

        protected override IQueryable<App.Projekte.Projekt> ApplyFilter(IQueryable<App.Projekte.Projekt> qry)
        {
            if (!string.IsNullOrWhiteSpace(ProjectName.Value))
                qry = qry.Where(p => p.Name.Contains(ProjectName.Value));

            return qry;
        }
    }
}
