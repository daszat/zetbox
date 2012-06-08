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
using Zetbox.App.GUI;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ValueViewModels;


namespace Zetbox.Client.WPF.View.GridCells
{
    /// <summary>
    /// Interaction logic for StringValue.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class MultilineStringValue : UserControl, IHasViewModel<MultiLineStringValueViewModel>
    {
        public MultilineStringValue()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<MultiLineStringPropertyModel> Members

        public MultiLineStringValueViewModel ViewModel
        {
            get { return (MultiLineStringValueViewModel)DataContext; }
        }

        #endregion
    }
}
