using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.App.Base;
using System.Collections.ObjectModel;

namespace Kistl.Client.PresenterModel
{
    /// <summary>
    /// Proxies a whole IDataObject
    /// </summary>
    public class DataObjectModel : PresentableModel
    {
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

        public DataObjectModel(IThreadManager uiManager, IThreadManager asyncManager, IDataObject obj)
            : base(uiManager, asyncManager)
        {
            _propertyModels = new ObservableCollection<PresentableModel>();
            _object = obj;
            _roContext = obj.Context.GetReadonlyContext();
            Async.Queue(FetchObjectClass);
        }

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

        private IDataObject _object;
        private IKistlContext _roContext;
        private ObjectClass _class;
    }
}
