using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using System.ComponentModel;

namespace Kistl.Client.PresenterModel
{
    public class ValuePropertyModel<TValue> : PresentableModel, IDataErrorInfo
        where TValue : class
    {
        public ValuePropertyModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj, BaseProperty bp)
            : base(uiManager, asyncManager)
        {
            _object = obj;
            _property = bp;
            _property.PropertyChanged += PropertyPropertyChanged;
            _object.PropertyChanged += ObjectPropertyChanged;
            Async.Queue(() => { this.CheckConstraints(); UI.Queue(() => { this.State = ModelState.Active; }); });
        }

        #region Public Interface

        public string Label { get { return _property.PropertyName; } }
        public string Tooltip { get { return _property.AltText; } }

        private TValue _valueCache;
        /// <summary>
        /// The value of the property presented by this model
        /// </summary>
        public TValue Value
        {
            get { UI.Verify(); return _valueCache; }
            set
            {
                UI.Verify();
                if (_valueCache != value)
                {
                    _valueCache = value;
                    State = ModelState.Loading;
                    Async.Queue(() => {
                        _object.SetPropertyValue<TValue>(_property.PropertyName, value);
                        CheckConstraints();
                        UI.Queue(() => this.State = ModelState.Active); });
                    OnPropertyChanged("Value");
                }
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        private void CheckConstraints()
        {
            Async.Verify();
            string newError = _object[_property.PropertyName];
            UI.Queue(() => this.ValueError = newError);
        }

        #endregion

        #region PropertyChanged event handlers

        private void ObjectPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();
            // flag to the user that something's happening
            UI.Queue(() => this.State = ModelState.Loading);

            if (e.PropertyName == _property.PropertyName)
            {
                TValue newValue = _object.GetPropertyValue<TValue>(_property.PropertyName);
                UI.Queue(() => Value = newValue);
            }
            // TODO: ask constraints about dependencies and reduce check frequency
            CheckConstraints();

            // all updates done
            UI.Queue(() => this.State = ModelState.Active);
        }

        private void PropertyPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Async.Verify();
            switch (e.PropertyName)
            {
                case "PropertyName": InvokePropertyChanged("Label"); break;
                case "AltText": InvokePropertyChanged("Tooltip"); break;
            }
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error { get { return this["Value"]; } }

        public string this[string columnName]
        {
            get
            {
                UI.Verify();
                if (columnName == "Value")
                    return ValueError;
                else
                    return null;
            }
        }

        private string _errorCache;
        protected string ValueError
        {
            get { UI.Verify(); return _errorCache; }
            set
            {
                UI.Verify();
                if (_errorCache != value)
                {
                    _errorCache = value;
                    // notify listeners that the error state of the Value has changed
                    OnPropertyChanged("Value");
                }
            }
        }

        #endregion

        private IDataObject _object;
        private BaseProperty _property;
    }
}
