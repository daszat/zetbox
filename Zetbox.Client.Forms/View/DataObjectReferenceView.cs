using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Client.Forms.View
{
    public partial class DataObjectReferenceView : DataObjectReferenceViewDesignerProxy
    {
        public DataObjectReferenceView()
        {
            InitializeComponent();
        }

        protected override void OnDataContextChanged()
        {
            base.OnDataContextChanged();
            Mock();
        }

        protected override void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnModelPropertyChanged(sender, e);
            if (e.PropertyName == "Label")
                Mock();
        }

        private void Mock()
        {
            label1.Text = DataContext.Label;
        }

    }

    public class DataObjectReferenceViewDesignerProxy : FormsUserControl<ObjectReferenceViewModel> { }

}
