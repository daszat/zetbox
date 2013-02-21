namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.App.Calendar;
    using System.ComponentModel;
    using Zetbox.App.GUI;

    [ViewModelDescriptor]
    public class EventViewModel : DataObjectViewModel
    {
        public new delegate EventViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Event evt);

        public EventViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Event evt)
            : base(appCtx, dataCtx, parent, evt)
        {
            this.Event = evt;
        }

        public Event Event { get; private set; }

        public override string Name
        {
            get { return Event.Summary; }
        }
    }
}
