using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Presentables
{
    public class SelectionTaskModel<TChoosable>
        : PresentableModel
        where TChoosable : PresentableModel
    {
        public SelectionTaskModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IList<TChoosable> choices,
            Action<TChoosable> callback)
            : base(appCtx, dataCtx)
        {
            _choices = new ReadOnlyCollection<TChoosable>(choices);
            _callback = callback;
        }

        #region Public interface

        public ReadOnlyCollection<TChoosable> Choices
        {
            get
            {
                return _choices;
            }
        }

        public void Choose(TChoosable choosen)
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

        private ReadOnlyCollection<TChoosable> _choices;
        private Action<TChoosable> _callback;
    }

    public class DataObjectSelectionTaskModel : SelectionTaskModel<DataObjectModel>
    {
        public DataObjectSelectionTaskModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            IList<DataObjectModel> choices,
            Action<DataObjectModel> callback)
            : base(appCtx, dataCtx, choices, callback)
        {
        }
    }
}
