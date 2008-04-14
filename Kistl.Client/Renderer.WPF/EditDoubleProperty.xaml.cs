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
    public partial class EditDoubleProperty : PropertyControl, IDoubleControl
    {

        public EditDoubleProperty()
        {
            InitializeComponent();
        }

        #region IDoubleControl Members

        double? IDoubleControl.Value
        {
            get
            {
                if (base.Value == null || base.Value.ToString().Length == 0)
                    return null;

                double result;
                if (double.TryParse(base.Value.ToString(), out result))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            set { base.Value = String.Format("{0}", value); }
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
            if (_UserInput != null)
            {
                _UserInput(this, new EventArgs());
            }
        }

        private event EventHandler _UserInput;
        event EventHandler IDoubleControl.UserInput
        {
            add { _UserInput += value; }
            remove { _UserInput -= value; }
        }

        #endregion

    }
}
