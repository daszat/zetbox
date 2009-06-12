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
        /// <summary>
        /// Initializes a new instance of the SelectionTaskModel class. This is protected since there 
        /// is no PresentableModelDescriptor for this class. Instead, either use the
        /// <see cref="DataObjectSelectionTaskModel"/> or inherit this for a specific type yourself and 
        /// add your own PresentableModelDescriptor and View.
        /// </summary>
        /// <param name="appCtx"></param>
        /// <param name="dataCtx"></param>
        /// <param name="choices"></param>
        /// <param name="callback"></param>
        protected SelectionTaskModel(
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
