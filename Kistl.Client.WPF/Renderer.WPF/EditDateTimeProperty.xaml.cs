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
using Kistl.GUI;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Zeigt eine einfache Eigenschaft zum Bearbeiten an
    /// </summary>
    public partial class EditDateTimeProperty : PropertyControl, IValueControl<DateTime?>
    {
        public EditDateTimeProperty()
        {
            InitializeComponent();
        }

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


        #region IDateTimeControl Members

        private bool _SupressUserInputEvent = false;
        DateTime? IValueControl<DateTime?>.Value
        {
            get { return (DateTime?)GetValue(ValueProperty); }
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
        public DateTime? Value
        {
            get { return (DateTime?)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(DateTime?), typeof(EditDateTimeProperty));


        public event EventHandler UserInput;

        public void FlagValidity(bool valid)
        {
            Panel.Background = valid ? Brushes.White : Brushes.Red;
        }

        #endregion


    }
}
