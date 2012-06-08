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
using System.Windows.Forms;
using System.ComponentModel;
using Zetbox.Client.Presentables;

namespace Zetbox.Client.Forms.View
{
    // mark as abstract, since the winforms designer cannot load the generics class anyways.
    // instead use a minimal "designer proxy" like this:
    //
    // public class DataObjectReferenceViewDesignerProxy : FormsUserControl<ObjectReferenceModel> { /* empty */ }
    // public partial class DataObjectReferenceView : DataObjectReferenceViewDesignerProxy {... } 
    // => this let's the designer instantiate a DataObjectReferenceViewDesignerProxy and therefore load the design surface
    public abstract class FormsUserControl<TModel> : UserControl, IFormsView
        where TModel : INotifyPropertyChanged
    {

        private TModel _dataContextCache;
        public TModel DataContext
        {
            get
            {
                return _dataContextCache;
            }
            internal set
            {
                if (_dataContextCache != null)
                {
                    throw new InvalidOperationException("DataContext may only be set once!");
                }

                _dataContextCache = value;
                _dataContextCache.PropertyChanged += OnModelPropertyChanged;
                OnDataContextChanged();
            }
        }

        protected virtual void OnDataContextChanged() { }
        protected virtual void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e) { }

        #region IFormsView Members

        void IFormsView.SetDataContext(INotifyPropertyChanged mdl)
        {
            this.DataContext = (TModel)mdl;
        }

        internal Renderer Renderer { get; private set; }
        void IFormsView.SetRenderer(Renderer r)
        {
            Renderer = r;
        }

        public void Activate()
        {
            Control win = this;
            while (!(win is Form))
            {
                win = win.Parent;
            }
            ((Form)win).Activate();
        }

        public void ShowDialog()
        {
            throw new NotSupportedException();
        }

        #endregion
    }
}
