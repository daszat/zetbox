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

        public int Project { get; set; }
    }
}
