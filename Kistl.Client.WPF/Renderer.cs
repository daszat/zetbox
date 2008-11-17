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

            var workspace = new WorkspaceView();
            workspace.DataContext = factory.CreateModel<WorkspaceModel>();
            workspace.Show();
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

        protected override void CreateWorkspace(WorkspaceModel mdl)
        {
            var workspace = new WorkspaceView();
            workspace.DataContext = mdl;
            workspace.Show();
        }

        protected override void CreateSelectionDialog(DataObjectSelectionTaskModel selectionTaskModel)
        {
            var selectionDialog = new SelectionDialog();
            selectionDialog.DataContext = selectionTaskModel;
            selectionDialog.Show();
        }
    }

}
