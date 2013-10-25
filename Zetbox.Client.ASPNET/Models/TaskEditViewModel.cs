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

    public class TaskEditViewModel : DataObjectEditViewModel<Zetbox.App.Projekte.Task>
    {
        public new delegate TaskEditViewModel Factory(IZetboxContext dataCtx, ViewModel parent);

        public TaskEditViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent)
            : base(appCtx, dataCtx, parent)
        {
        }

        public int ProjectID { get; set; }

        public DataObjectViewModel ProjectViewModel
        {
            get
            {
                if (ProjectID != default(int))
                    return DataObjectViewModel.Fetch(ViewModelFactory, DataContext, this, DataContext.Find<Zetbox.App.Projekte.Projekt>(ProjectID));
                else 
                    return null;
            }
        }
    }
}
