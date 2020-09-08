using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zetbox.API;
using Zetbox.App.Projekte;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.ViewModels
{
    public class MvcProjekteListViewModel : GenericSearchViewModel<Projekt, DataObjectViewModel>
    {
        public new delegate MvcProjekteListViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public MvcProjekteListViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }
    }
}
