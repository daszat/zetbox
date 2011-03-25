
namespace Kistl.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.API.Configuration;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;

    class TestViewModelFactory
        : ViewModelFactory
    {
        public TestViewModelFactory(Autofac.ILifetimeScope container,
            IFrozenContext frozenCtx,
            KistlConfig cfg)
            : base(container, frozenCtx, cfg)
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

        protected override void ShowWaitDialog()
        {
        }

        protected override void CloseWaitDialog()
        {
        }
    }
}
