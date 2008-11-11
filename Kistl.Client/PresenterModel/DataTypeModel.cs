using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Kistl.App.Base;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client.PresenterModel
{
    public class DataTypeModel : DataObjectModel
    {
        public DataTypeModel(IThreadManager uiManager, IThreadManager asyncManager, DataType type)
            : base(uiManager, asyncManager, type)
        {
            _type = type;
            // wtf am I doing here?
            // TODO: split usage of this P-Model from usage elsewhere?!
            if (AsyncContext == null)
                AsyncContext = _type.Context is FrozenContext ? KistlContext.GetContext() : _type.Context;
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
                    Async.Queue(AsyncContext, LoadInstances);
                }
                return _instances;
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected virtual void QueryHasInstances()
        {
            Async.Verify();
            UI.Queue(UI, () => { HasInstances = false; State = ModelState.Active; });
        }

        protected virtual void LoadInstances()
        {
            Async.Verify();
            UI.Queue(UI, () => { State = ModelState.Active; });

        }

        /// <returns>the default icon of this <see cref="DataType"/></returns>
        protected override Kistl.App.GUI.Icon AsyncGetIcon()
        {
            return _type.DefaultIcon;
        }

        #endregion

        private DataType _type;
        protected static IKistlContext AsyncContext { get; private set; }
    }
}
