using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.View.ZetboxBase;

namespace Zetbox.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for NullablePropertyMultiLineTextBoxView.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class NullablePropertyMultiLineTextBoxView : PropertyEditor, IHasViewModel<IValueViewModel<string>>
    {
        public NullablePropertyMultiLineTextBoxView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }


        #region IHasViewModel<IValueModel<string>> Members

        public IValueViewModel<string> ViewModel
        {
            get { return (IValueViewModel<string>)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return txt; }
        }
    }
}
