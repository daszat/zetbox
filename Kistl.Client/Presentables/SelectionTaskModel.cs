using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Presentables
{
    public class SelectionTaskModel<TModel>
        : PresentableModel
        where TModel : PresentableModel
    {
        public SelectionTaskModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IList<TModel> choices,
            Action<TModel> callback)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory)
        {
            _choices = new ReadOnlyCollection<TModel>(choices);
            _callback = callback;
        }

        #region Public interface

        public ReadOnlyCollection<TModel> Choices
        {
            get
            {
                UI.Verify();
                return _choices;
            }
        }

        public void Choose(TModel choosen)
        {
            if (_choices.Contains(choosen))
            {
                _callback(choosen);
            }
            else
            {
                _callback(null);
            }
        }

        #endregion

        private ReadOnlyCollection<TModel> _choices;
        private Action<TModel> _callback;
    }

    public class DataObjectSelectionTaskModel : SelectionTaskModel<DataObjectModel>
    {
        public DataObjectSelectionTaskModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            IList<DataObjectModel> choices,
            Action<DataObjectModel> callback)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, choices, callback)
        {
        }
    }
}
