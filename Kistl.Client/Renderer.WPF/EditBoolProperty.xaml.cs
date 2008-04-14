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
    /// Zeigt eine Bool Eigenschaft zum Bearbeiten an
    /// </summary>
    public partial class EditBoolProperty : PropertyControl, IBoolControl
    {
        public EditBoolProperty()
        {
            InitializeComponent();
        }

        #region IBoolControl Members

        bool? IBoolControl.Value
        {
            get { return (bool?)base.Value; }
            set { base.Value = value; }
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
        event EventHandler IBoolControl.UserInput
        {
            add { _UserInput += value; }
            remove { _UserInput -= value; }
        }

        #endregion

    }
}
