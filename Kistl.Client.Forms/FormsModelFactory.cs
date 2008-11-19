using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Kistl.Client.Forms.View;
using Kistl.Client.Presentables;

namespace Kistl.Client.Forms
{
    public class FormsModelFactory : ModelFactory
    {

        private List<WorkspaceView> _workspaces = new List<WorkspaceView>();

        public FormsModelFactory(IGuiApplicationContext appCtx)
            : base(appCtx)
        {
        }

        protected override void CreateSelectionDialog(DataObjectSelectionTaskModel selectionTaskModel, bool activate)
        {
            throw new NotImplementedException();
        }

        protected override void CreateWorkspace(WorkspaceModel mdl, bool activate)
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

        protected override void ShowDataObject(DataObjectModel dataObject, bool activate)
        {
            throw new NotImplementedException();
        }
    }
}
