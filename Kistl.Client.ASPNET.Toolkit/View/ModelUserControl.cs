using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Kistl.API;
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit.View
{
    public abstract class ModelUserControl<T> : System.Web.UI.UserControl, IView
        where T : class
    {
        private T _Model = null;
        public T Model 
        {
            get
            {
                if (_Model == null && !string.IsNullOrEmpty(ModelPath))
                {
                    // Search in Parent
                    Control ctrl = this.Parent;
                    while (ctrl != null)
                    {
                        if (ctrl.HasProperty(ModelPath))
                        {
                            object mdl = ctrl.GetPropertyValue<object>(ModelPath);
                            if (mdl is T)
                            {
                                _Model = (T)mdl;
                                break;
                            }
                        }
                        ctrl = ctrl.Parent;
                    }

                    if (_Model == null)
                    {
                        // Still null -> not found
                        throw new ArgumentOutOfRangeException("ModelPath", string.Format("Unable to find Model with ModelPath {0}", ModelPath));
                    }
                }
                return _Model;
            }
        }

        public string ModelPath { get; set; }

        public virtual void SetModel(PresentableModel mdl)
        {
            if (!(mdl is T)) throw new ArgumentOutOfRangeException("Incompatible Model was set");
            _Model = (T)(object)mdl;
        }
    }
}
