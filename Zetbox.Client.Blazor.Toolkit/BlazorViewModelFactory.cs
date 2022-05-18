
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

    /// <summary>
    /// ViewModelFactory for Blazor
    /// </summary>
	public class BlazorViewModelFactory : ViewModelFactory
    {
        private readonly NavigationManager navigationManager;

        /// <inheritdoc />
        public BlazorViewModelFactory(ILifetimeScopeFactory scopeFactory, Autofac.ILifetimeScope scope, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter, DialogCreator.Factory dialogFactory, NavigationManager navigationManager)
            : base(scopeFactory, scope, frozenCtx, cfg, perfCounter, dialogFactory)
        {
            this.navigationManager = navigationManager;
        }

        /// <inheritdoc />
        public override App.GUI.Toolkit Toolkit
        {
            get { return App.GUI.Toolkit.Blazor; }
        }

        /// <summary>
        /// TODO: This is not the best option....
        /// </summary>
        public static Presentables.ObjectEditor.WorkspaceViewModel? LastObjectEditorWorkspace { get; private set; }

        /// <inheritdoc />
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

        /// <inheritdoc />
		protected override object CreateView(ViewDescriptor vDesc)
		{
            // return the type instead
			return Type.GetType(vDesc.ControlTypeRef)!;
		}

        /// <inheritdoc />
		public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override string GetSourceFileNameFromUser(params string[] filter)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override string GetDestinationFileNameFromUser(string filename, params string[] filter)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override bool GetDecisionFromUser(string message, string caption)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override void ShowMessage(string message, string caption)
        {
            throw new NotSupportedException();
        }

        /// <inheritdoc />
        public override TaskScheduler UITaskScheduler => TaskScheduler.Default;
    }
}
