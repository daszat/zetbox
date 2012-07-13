// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Forms
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Forms;
    using Zetbox.API;
    using Zetbox.Client.Forms.View;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ObjectBrowser;
    using Zetbox.API.Configuration;
    using Zetbox.API.Client.PerfCounter;

    public class FormsModelFactory
        : ViewModelFactory
    {

        private List<WorkspaceView> _workspaces = new List<WorkspaceView>();

        public FormsModelFactory(Autofac.ILifetimeScope container, IFrozenContext metaCtx, ZetboxConfig cfg, IPerfCounter perfCounter, Func<DialogCreator> dialogFactory)
            : base(container, metaCtx, cfg, perfCounter, dialogFactory)
        {
        }

        public override Zetbox.App.GUI.Toolkit Toolkit
        {
            get { return Zetbox.App.GUI.Toolkit.WinForms; }
        }

        protected void CreateWorkspace(WorkspaceViewModel mdl, bool activate)
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

        protected override void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog)
        {
            var control = (IFormsView)view;
            control.SetRenderer(_renderer);
            control.SetDataContext(mdl);
            if (asDialog)
            {
                control.ShowDialog();
            }
            else
            {
                control.Show();
                if (activate)
                    control.Activate();
            }
        }

        private Renderer _renderer = new Renderer();

        public override void CreateTimer(TimeSpan tickLength, Action action)
        {
            throw new NotImplementedException();
        }

        public override string GetSourceFileNameFromUser(params string[] filters)
        {
            throw new NotImplementedException();
        }
        public override string GetDestinationFileNameFromUser(string filename, params string[] filters)
        {
            throw new NotImplementedException();
        }

        public override bool GetDecisionFromUser(string message, string caption)
        {
            return MessageBox.Show(message, caption, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public override void ShowMessage(string message, string caption)
        {
            MessageBox.Show(message, caption);
        }
    }
}
