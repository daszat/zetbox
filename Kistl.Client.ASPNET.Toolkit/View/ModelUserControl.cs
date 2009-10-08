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
        public T Model 
        {
            get
            {
                return (T)(object)GetModel();
            }
        }

        public string ModelPath { get; set; }

        private PresentableModel _Model = null;

        public virtual void SetModel(PresentableModel mdl)
        {
            if (!(mdl is T)) throw new ArgumentOutOfRangeException("Incompatible Model was set");
            _Model = mdl;
        }

        public PresentableModel GetModel()
        {
            if (ApplicationContext.Current == null) return null; // Designmode
            if (_Model == null && !string.IsNullOrEmpty(ModelPath))
            {
                // Search in Parent
                Control ctrl = this.Parent;
                while (ctrl != null)
                {
                    IView view = ctrl as IView;
                    if (view != null && view.GetModel().HasProperty(ModelPath))
                    {
                        object mdl = view.GetModel().GetPropertyValue<object>(ModelPath);
                        if (mdl is T)
                        {
                            _Model = (PresentableModel)mdl;
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
}
