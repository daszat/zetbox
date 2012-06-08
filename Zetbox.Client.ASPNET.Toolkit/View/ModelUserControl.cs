// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using Zetbox.API;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.ASPNET.Toolkit.View
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

        private ViewModel _Model = null;

        public virtual void SetModel(ViewModel mdl)
        {
            if (!(mdl is T)) throw new ArgumentOutOfRangeException("mdl", "Incompatible Model was set");
            _Model = mdl;
        }

        public ViewModel GetModel()
        {
            if (ZetboxContextManagerModule.ViewModelFactory == null) return null; // Designmode
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
                            _Model = (ViewModel)mdl;
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
