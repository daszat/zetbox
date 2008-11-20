using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.Client.Presentables;
using Kistl.Client.WPF.View;
using System.Windows;

namespace Kistl.Client.WPF
{
    public class Renderer
    {
        private static Renderer _current;
        public static Renderer Current
        {
            get
            {
                if (_current == null)
                    _current = new Renderer();

                return _current;
            }
        }

    }

    public class WpfModelFactory : ModelFactory
    {
        public WpfModelFactory(IGuiApplicationContext appCtx)
            : base(appCtx)
        {
        }

        private static WorkspaceModel _mdl;
        protected void CreateWorkspace(WorkspaceModel mdl, bool activate)
        {
            _mdl = mdl;
            var workspace = new WorkspaceView(); // TODO: delegate to data store / TypeDescriptor
            workspace.DataContext = mdl;
            workspace.ShowActivated = activate;
            workspace.Show();
        }

        protected void CreateSelectionDialog(DataObjectSelectionTaskModel selectionTaskModel, bool activate)
        {
            var selectionDialog = new SelectionDialog(); // TODO: delegate to data store / TypeDescriptor
            selectionDialog.DataContext = selectionTaskModel;
            selectionDialog.ShowActivated = activate;
            selectionDialog.Show();
        }


        protected void ShowDataObject(DataObjectModel dataObject, bool activate)
        {
            _mdl.HistoryTouch(dataObject);
            if (activate)
                _mdl.SelectedItem = dataObject;
        }

        protected override Kistl.App.GUI.Toolkit Toolkit
        {
            get { return Kistl.App.GUI.Toolkit.WPF; }
        }

        protected override void ShowInView(object renderer, PresentableModel mdl, object view, bool activate)
        {
            var viewControl = (Window)view;
            viewControl.DataContext = mdl;
            viewControl.ShowActivated = activate;
            viewControl.Show();
        }

        protected override object Renderer { get { return null; } }
    }

}
