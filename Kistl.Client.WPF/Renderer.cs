using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.Client.Presentables;
using Kistl.Client.WPF.View;

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
        protected override void CreateWorkspace(WorkspaceModel mdl, bool activate)
        {
            _mdl = mdl;
            var workspace = new WorkspaceView(); // TODO: delegate to data store / TypeDescriptor
            workspace.DataContext = mdl;
            workspace.ShowActivated = activate;
            workspace.Show();
        }

        protected override void CreateSelectionDialog(DataObjectSelectionTaskModel selectionTaskModel, bool activate)
        {
            var selectionDialog = new SelectionDialog(); // TODO: delegate to data store / TypeDescriptor
            selectionDialog.DataContext = selectionTaskModel;
            selectionDialog.ShowActivated = activate;
            selectionDialog.Show();
        }


        protected override void ShowDataObject(DataObjectModel dataObject, bool activate)
        {
            _mdl.HistoryTouch(dataObject);
            if (activate)
                _mdl.SelectedItem = dataObject;
        }
    }

}
