using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Kistl.Client.Controls
{
    /// <summary>
    /// Zeigt eine Bool Eigenschaft zum Bearbeiten an
    /// </summary>
    public partial class EditBoolProperty : UserControl
    {
        public EditBoolProperty()
        {
            InitializeComponent();
            Label = "Label";
            Value = false;
        }

        /// <summary>
        /// Bezeichnung der Eigenschaft
        /// </summary>
        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Label.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(EditBoolProperty));

        /// <summary>
        /// Wert der Eigenschaft
        /// </summary>
        public bool? Value
        {
            get { return (bool?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(bool?), typeof(EditBoolProperty));
    }
}
