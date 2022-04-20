﻿
namespace Zetbox.Client.Blazor
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

    public class BlazorViewModelFactory : ViewModelFactory
    {
        public BlazorViewModelFactory(ILifetimeScopeFactory scopeFactory, Autofac.ILifetimeScope scope, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter, DialogCreator.Factory dialogFactory)
            : base(scopeFactory, scope, frozenCtx, cfg, perfCounter, dialogFactory)
        {
        }

        public override App.GUI.Toolkit Toolkit
        {
            get { return App.GUI.Toolkit.Blazor; }
        }

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog, ViewModel ownerMdl)
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
