namespace Zetbox.App.Projekte.Client.ViewModel.TestModule
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.Calendar;
    using System.ComponentModel;

    [ViewModelDescriptor]
    public class EventTestInputViewModel : ViewModel, IEventInputViewModel
    {
        public new delegate EventTestInputViewModel Factory(IZetboxContext dataCtx, ViewModel parent, DateTime selectedStartDate);

        public EventTestInputViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, DateTime selectedStartDate)
            : base(appCtx, dataCtx, parent)
        {
            // Zetbox.App.Test.EventTestObject
        }

        public override string Name
        {
            get { return "EventTestInputViewModel"; }
        }

        public EventViewModel CreateNew()
        {
            throw new NotImplementedException();
        }

        public string Error
        {
            get { return "Not implemented"; }
        }

        string IDataErrorInfo.this[string columnName]
        {
            get { return string.Empty; }
        }
    }
}
