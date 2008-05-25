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
    public partial class EditSimpleProperty : PropertyControl, IValueControl<string>
    {

        public EditSimpleProperty()
        {
            InitializeComponent();
        }

        #region IStringControl Members

        private bool _SupressUserInputEvent = false;
        string IValueControl<string>.Value
        {
            get { return (string)GetValue(ValueProperty); }
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
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(EditSimpleProperty));

        public event EventHandler UserInput;


        public void FlagValidity(bool valid)
        {
            Panel.Background = valid ? Brushes.White : Brushes.Red;
        }

        #endregion

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

    }
}
