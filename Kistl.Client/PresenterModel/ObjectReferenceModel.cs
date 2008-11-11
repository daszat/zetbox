using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{

    public class ObjectReferenceModel
        : PropertyModel<DataObjectModel>
    {
        public ObjectReferenceModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject referenceHolder, ObjectReferenceProperty prop)
            : base(uiManager, asyncManager, referenceHolder, prop)
        { }


        #region Public Interface

        public bool IsNull
        {
            get
            {
                UI.Verify();
                return _valueCache == null;
            }
        }

        private DataObjectModel _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override DataObjectModel Value
        {
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(Object.Context, () =>
                {
                    Object.SetPropertyValue<IDataObject>(Property.PropertyName, _valueCache.Object);
                    CheckConstraints();
                    UI.Queue(UI, () => this.State = ModelState.Active);
                });
                OnPropertyChanged("Value");
                OnPropertyChanged("IsNull");
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        protected override void GetPropertyValue()
        {
            Async.Verify();
            IDataObject newValue = Object.GetPropertyValue<IDataObject>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue == null ? null : new DataObjectModel(UI, Async, newValue));
        }

        #endregion

    }
}
