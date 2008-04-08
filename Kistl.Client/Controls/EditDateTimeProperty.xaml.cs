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

namespace Kistl.Client.Controls
{
    /// <summary>
    /// Zeigt eine einfache Eigenschaft zum Bearbeiten an
    /// </summary>
    public partial class EditDateTimeProperty : PropertyControl, IDateTimeControl
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
                OnValueChanged(e);
            }
        }

        protected virtual void OnValueChanged(DependencyPropertyChangedEventArgs e)
        {
            if (_ValueChanged != null)
            {
                _ValueChanged(this, new EventArgs());
            }
        }


        #region IDateTimeControl Members

        DateTime? IDateTimeControl.Value
        {
            get
            {
                return (DateTime?)this.Value;
            }
            set
            {
                this.Value = value;
            }
        }

        private event EventHandler _ValueChanged;
        event EventHandler IDateTimeControl.ValueChanged
        {
            add { _ValueChanged += value; }
            remove { _ValueChanged -= value; }
        }


        #endregion


    }
}
