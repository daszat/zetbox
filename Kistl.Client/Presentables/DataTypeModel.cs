using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Kistl.App.Base;
using Kistl.API;
using Kistl.API.Client;
using System.Collections.Specialized;

namespace Kistl.Client.Presentables
{
    public class DataTypeModel : DataObjectModel
    {
        public DataTypeModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            DataType type)
            : base(appCtx, dataCtx, type)
        {
            _type = type;
            // refresh Icon
            Async.Queue(DataContext, AsyncUpdateViewCache);
        }

        #region Public interface

        private bool _hasInstances = false;
        public bool HasInstances
        {
            get
            {
                UI.Verify();
                State = ModelState.Loading;
                Async.Queue(DataContext, () =>
                {
                    AsyncQueryHasInstances();
                    // TODO: Wird auch in der Ableitung aufgerufen.
                    // Stateobjekt als IDisposable durchreichen
                    // aber dann braucht man einen ReferenceCounter - Uaaaaa
                    // was nochmals eine andere Klasse von Scheiße ist.
                    // Aber man könnte dem einen String umhängen, was geladen wurde.
                    // Umpriorisieren & Abbrechen wäre dann auch möglich.
                    // Case: 661
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
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

        // TODO: make readonly, take care of new and deleted+submitted objects
        private ObservableCollection<DataObjectModel> _instances = null;
        public ObservableCollection<DataObjectModel> Instances
        {
            get
            {
                UI.Verify();
                if (_instances == null)
                {
                    _instances = new ObservableCollection<DataObjectModel>();
                    // As this is a "calculated" collection, only insider should modify this
                    //_instances.CollectionChanged += _instances_CollectionChanged;
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
