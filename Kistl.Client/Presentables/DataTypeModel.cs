using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Kistl.App.Base;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client.Presentables
{
    public class DataTypeModel : DataObjectModel
    {
        public DataTypeModel(
            IThreadManager uiManager, IThreadManager asyncManager,
            IKistlContext guiCtx, IKistlContext dataCtx,
            ModelFactory factory,
            DataType type)
            : base(uiManager, asyncManager, guiCtx, dataCtx, factory, type)
        {
            _type = type;

            Async.Queue(DataContext, AsyncUpdateViewCache);
        }

        #region Public interface

        private bool _hasInstances = false;
        public bool HasInstances
        {
            get
            {
                UI.Verify();
                return _hasInstances;
            }
            protected set
            {
                UI.Verify();
                if (value != _hasInstances)
                {
                    _hasInstances = value;
                    OnPropertyChanged("HasInstances");
                }
            }
        }

        private ObservableCollection<DataObjectModel> _instances = null;
        public ObservableCollection<DataObjectModel> Instances
        {
            get
            {
                UI.Verify();
                if (_instances == null)
                {
                    _instances = new ObservableCollection<DataObjectModel>();
                    Async.Queue(DataContext, AsyncLoadInstances);
                }
                return _instances;
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        /// <summary>
        /// Sets the HasInstances property to the appropriate value
        /// </summary>
        protected virtual void AsyncQueryHasInstances()
        {
            Async.Verify();
            UI.Queue(UI, () => { HasInstances = false; State = ModelState.Active; });
        }

        /// <summary>
        /// Loads the instances of this DataType and adds them to the Instances collection
        /// </summary>
        protected virtual void AsyncLoadInstances()
        {
            Async.Verify();
            UI.Queue(UI, () => { State = ModelState.Active; });

        }

        /// <returns>the default icon of this <see cref="DataType"/></returns>
        protected override Kistl.App.GUI.Icon AsyncGetIcon()
        {
            Async.Verify();
            if (_type != null)
                return _type.DefaultIcon;
            else
                return null;
        }

        #endregion

        private DataType _type;
    }
}
