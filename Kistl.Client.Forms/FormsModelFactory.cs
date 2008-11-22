using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kistl.Client.Forms.View;
using Kistl.Client.Presentables;
using Kistl.Client.GUI;

namespace Kistl.Client.Forms
{
    public class FormsModelFactory : ModelFactory
    {

        private List<WorkspaceView> _workspaces = new List<WorkspaceView>();

        public FormsModelFactory(IGuiApplicationContext appCtx)
            : base(appCtx)
        {
        }

        protected override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.TEST; }
        }

        protected void CreateWorkspace(WorkspaceModel mdl, bool activate)
        {
            var workspace = new WorkspaceView(); // TODO: delegate to data store / TypeDescriptor
            workspace.DataContext = mdl;
            workspace.Closed += new EventHandler(workspace_Closed);
            workspace.Show();
            _workspaces.Add(workspace);
        }

        void workspace_Closed(object sender, EventArgs e)
        {
            _workspaces.Remove((WorkspaceView)sender);
            if (_workspaces.Count == 0)
                Application.Exit();
        }

        protected override void ShowInView(object renderer, PresentableModel mdl, IView view, bool activate)
        {
            var control = (IFormsView)view;
            control.SetRenderer((Renderer)renderer);
            control.SetDataContext(mdl);
            control.Show();
            if (activate)
                control.Activate();
        }

        private Renderer _renderer = new Renderer();
        protected override object Renderer { get { return _renderer; } }
    }
}
