
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
    using Microsoft.AspNetCore.Components;
	using Zetbox.App.GUI;

	public class BlazorViewModelFactory : ViewModelFactory
    {
        private readonly NavigationManager navigationManager;

        public BlazorViewModelFactory(ILifetimeScopeFactory scopeFactory, Autofac.ILifetimeScope scope, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter, DialogCreator.Factory dialogFactory, NavigationManager navigationManager)
            : base(scopeFactory, scope, frozenCtx, cfg, perfCounter, dialogFactory)
        {
            this.navigationManager = navigationManager;
        }

        public override App.GUI.Toolkit Toolkit
        {
            get { return App.GUI.Toolkit.Blazor; }
        }

        // TODO: This is not the best option....
        public static Presentables.ObjectEditor.WorkspaceViewModel LastObjectEditorWorkspace { get; private set; }

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog, ViewModel ownerMdl)
        {
            if (mdl is Presentables.ObjectEditor.WorkspaceViewModel workspace)
            {
                LastObjectEditorWorkspace = workspace;
                navigationManager.NavigateTo($"/ObjectEditor");
            }
			else
			{
                throw new NotSupportedException($"Cannot show viewmodel {mdl.GetType().FullName}");
			}
        }

		protected override object CreateView(ViewDescriptor vDesc)
		{
            // return the type instead
			return Type.GetType(vDesc.ControlTypeRef);
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

        public override TaskScheduler UITaskScheduler => TaskScheduler.Default;
    }
}
