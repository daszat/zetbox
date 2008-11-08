using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    public class ObjectReferenceModel<TValue>
        : PropertyModel<TValue>
        where TValue : IDataObject
    {
        public ObjectReferenceModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj, ObjectReferenceProperty prop)
            : base(uiManager, asyncManager, obj, prop)
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

        private TValue _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public override TValue Value
        {
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();

                _valueCache = value;
                State = ModelState.Loading;
                Async.Queue(Object.Context, () =>
                {
                    Object.SetPropertyValue<TValue>(Property.PropertyName, value);
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
            TValue newValue = Object.GetPropertyValue<TValue>(Property.PropertyName);
            UI.Queue(UI, () => Value = newValue);
        }

        #endregion

    }
}
