namespace Zetbox.Client.Presentables.Calendar
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Calendar;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;

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

        protected override void OnPropertyModelsByNameCreated()
        {
            base.OnPropertyModelsByNameCreated();

            var startDateVmdl = (NullableDateTimePropertyViewModel)PropertyModelsByName["StartDate"];
            startDateVmdl.InputAccepted += (s, e) =>
            {
                if (e.NewValue.HasValue && e.OldValue.HasValue)
                {
                    Event.EndDate = Event.EndDate + (e.NewValue.Value - e.OldValue.Value);
                }
            };
        }

        public override string Name
        {
            get { return Event.Summary; }
        }
    }
}
