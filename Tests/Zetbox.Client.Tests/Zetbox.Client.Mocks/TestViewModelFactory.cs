
namespace Zetbox.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Configuration;
    using Zetbox.App.GUI;
    using Zetbox.Client.Presentables;
using Zetbox.API.Client.PerfCounter;

    class TestViewModelFactory
        : ViewModelFactory
    {
        public TestViewModelFactory(Autofac.ILifetimeScope container,
            IFrozenContext frozenCtx,
            ZetboxConfig cfg, IPerfCounter perfCounter)
            : base(container, frozenCtx, cfg, perfCounter)
        {
        }

        public override Toolkit Toolkit
        {
            get { return Toolkit.TEST; }
        }

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
        {
        }

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
        }

        public override string GetSourceFileNameFromUser(params string[] filter)
        {
            return null;
        }

        public override string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            return null;
        }

        public override bool GetDecisionFromUser(string message, string caption)
        {
            return false;
        }

        public override void ShowMessage(string message, string caption)
        {
        }
    }
}
