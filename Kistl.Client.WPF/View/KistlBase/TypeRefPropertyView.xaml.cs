
namespace Kistl.Client.WPF.View.KistlBase
{
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

    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.KistlBase;

    /// <summary>
    /// Interaction logic for TypeRefPropertyView.xaml
    /// </summary>
    public partial class TypeRefPropertyView
        : PropertyEditor
    {
        public TypeRefPropertyView()
        {
            InitializeComponent();
        }

        #region IView Members

        public void SetModel(ViewModel mdl)
        {
            DataContext = (TypeRefPropertyViewModel)mdl;
        }

        #endregion
    }
}
