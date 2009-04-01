using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Utils;

namespace Kistl.Client.Presentables
{

    /// <summary>
    /// Models a group of Property(Models)
    /// </summary>
    public class PropertyGroupModel
        : PresentableModel
    {
        private string _title;
        private ObservableCollection<PresentableModel> _properties;

        public PropertyGroupModel(
            IGuiApplicationContext appCtx, IKistlContext dataCtx,
            string title,
            IEnumerable<PresentableModel> obj)
            : base(appCtx, dataCtx)
        {
            _title = title;
            _properties = new ObservableCollection<PresentableModel>(obj);
        }

        #region Public Interface

        public string Title { get { return _title; } }
        public string ToolTip { get { return _title; } }

        private ReadOnlyObservableCollection<PresentableModel> _propertyModelsCache;
        public ReadOnlyObservableCollection<PresentableModel> PropertyModels
        {
            get
            {
                if (_propertyModelsCache == null)
                {
                    _propertyModelsCache = new ReadOnlyObservableCollection<PresentableModel>(_properties);
                }
                return _propertyModelsCache;
            }
        }

        #endregion

    }
}
