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

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Zeigt eine einfache Eigenschaft zum Bearbeiten an
    /// </summary>
    public partial class EditIntProperty : PropertyControl, IValueControl<int?>
    {
        public EditIntProperty()
        {
            InitializeComponent();
        }

        #region IValueControl<int?> Members

        private bool _SupressUserInputEvent = false;
        int? IValueControl<int?>.Value
        {
            get { return (int?)GetValue(ValueProperty); }
            set
            {
                try
                {
                    _SupressUserInputEvent = true;
                    SetValue(ValueProperty, value);
                }
                finally
                {
                    _SupressUserInputEvent = false;
                }
            }
        }

        /// <summary>
        /// The actual Value of this Property
        /// </summary>
        public int? Value
        {
            get { return (int?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(int?), typeof(EditIntProperty));

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property == ValueProperty)
            {
                OnUserInput(e);
            }
        }

        protected virtual void OnUserInput(DependencyPropertyChangedEventArgs e)
        {
            if (UserInput != null && !_SupressUserInputEvent)
            {
                UserInput(this, new EventArgs());
            }
        }

        public event EventHandler UserInput;

        public void FlagValidity(bool valid)
        {
            Panel.Background = valid ? Brushes.White : Brushes.Red;
        }

        #endregion

    }
}
