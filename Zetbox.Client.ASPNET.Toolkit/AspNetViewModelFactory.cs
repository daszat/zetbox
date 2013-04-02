
namespace Zetbox.Client.ASPNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.Client.GUI;

    public class AspNetViewModelFactory : ViewModelFactory
    {
        public AspNetViewModelFactory(Autofac.ILifetimeScope container, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter, DialogCreator.Factory dialogFactory)
            : base(container, frozenCtx, cfg, perfCounter, dialogFactory)
        {
        }

        public override App.GUI.Toolkit Toolkit
        {
            get { return App.GUI.Toolkit.ASPNET; }
        }

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
        {
            throw new NotSupportedException();
        }

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            throw new NotSupportedException();
        }

        public override string GetSourceFileNameFromUser(params string[] filter)
        {
            throw new NotSupportedException();
        }

        public override string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            throw new NotSupportedException();
        }

        public override bool GetDecisionFromUser(string message, string caption)
        {
            throw new NotSupportedException();
        }

        public override void ShowMessage(string message, string caption)
        {
            throw new NotSupportedException();
        }
    }
}
