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

using Kistl.Client.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for ObjectReferenceView.xaml
    /// </summary>
    public partial class ObjectReferenceView : UserControl, IView
    {
        public ObjectReferenceView()
        {
            InitializeComponent();
        }

        private void OpenSelection(object sender, RoutedEventArgs e)
        {
            var model = (ObjectReferenceModel)DataContext;
            model.OpenReference();
        }

        private void ClearValueHandler(object sender, RoutedEventArgs e)
        {
            var mdl = (IClearableValue)DataContext;
            mdl.ClearValue();
        }

        private void CreateNewHandler(object sender, RoutedEventArgs e)
        {
            var model = (ObjectReferenceModel)DataContext;
            model.CreateNew();
        }

        #region IView Members

        public void SetModel(PresentableModel mdl)
        {
            DataContext = (ObjectReferenceModel)mdl;
        }

        #endregion

    }
}
