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
