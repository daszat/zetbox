using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zetbox.API;
using Zetbox.App.Projekte;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.ViewModels
{
    public class MvcProjektListViewModel : GenericSearchViewModel<Projekt, DataObjectViewModel>
    {
        public new delegate MvcProjektListViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public MvcProjektListViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public string NamePart { get; set; }

        protected override IQueryable<Projekt> ApplyFilter(IQueryable<Projekt> qry)
        {
            qry = base.ApplyFilter(qry);

            if (!string.IsNullOrWhiteSpace(NamePart))
            {
                qry = qry.Where(i => i.Name.ToLower().Contains(NamePart.ToLower()));
            }

            return qry;
        }

        protected override IQueryable<Projekt> ApplyOrderBy(IQueryable<Projekt> qry)
        {
            return qry.OrderBy(i => i.Name).ThenBy(i => i.ID);
        }
    }
}
