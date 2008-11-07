using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.PresenterModel
{
    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    public class DataObjectModel : PresentableModel
    {
        public DataObjectModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj)
            : base(uiManager, asyncManager)
        {
            _propertyModels = new ObservableCollection<PresentableModel>();
            _object = obj;
            _roContext = obj.Context.GetReadonlyContext();
            Async.Queue(() => { FetchObjectClass(); UI.Queue(() => this.State = ModelState.Active); });
        }

        #region Public Interface

        private ObservableCollection<PresentableModel> _propertyModels;
        public ObservableCollection<PresentableModel> PropertyModels
        {
            get
            {
                UI.Verify();
                return _propertyModels;
            }
            private set
            {
                UI.Verify();
                if (value != _propertyModels)
                {
                    _propertyModels = value;
                    OnPropertyChanged("PropertyModels");
                }
            }
        }

        #endregion

        #region Async handlers and UI callbacks

        private void FetchObjectClass()
        {
            Async.Verify();
            ObjectClass cls = _object.GetObjectClass(_roContext);
            var props = cls.Properties;
            UI.Queue(() => SetClassPropertyModels(cls, props));
        }

        private void SetClassPropertyModels(ObjectClass cls, IEnumerable<BaseProperty> props)
        {
            UI.Verify();
            _class = cls;
            foreach (var pm in props)
            {
                if (pm is StringProperty)
                {
                    PropertyModels.Add(new ValuePropertyModel<string>(UI, Async, _object, pm));
                }
            }
        }

        #endregion

        private IDataObject _object;
        private IKistlContext _roContext;
        private ObjectClass _class;
    }
}
