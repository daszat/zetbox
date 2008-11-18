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

        public void CreateWorkspace()
        {
            //var factory = new ModelFactory(
            //    new UiThreadManager(),
            //    new AsyncThreadManager(),
            //    FrozenContext.Single,
            //    KistlContext.GetContext());

            var factory = new WpfModelFactory(KistlContext.GetContext());
            factory.ShowModel(
                factory.CreateModel<WorkspaceModel>(), true);
        }
    }

    internal class WpfModelFactory : ModelFactory
    {
        internal WpfModelFactory(IKistlContext dataContext)
            : base(
                new SynchronousThreadManager(), new SynchronousThreadManager(),
                FrozenContext.Single, dataContext)
        {
        }

        public override ModelFactory CreateNewFactory(IKistlContext newDataCtx)
        {
            return new WpfModelFactory(newDataCtx);
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
