using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace Kistl.Client.PresenterModel
{
    public class WorkspaceModel : PresentableModel
    {
        public WorkspaceModel(IThreadManager uiManager, IThreadManager asyncManager)
            : base(uiManager, asyncManager)
        {
        }

        public ObservableCollection<DataObjectModel> Objects { get; private set; }
    }
}
