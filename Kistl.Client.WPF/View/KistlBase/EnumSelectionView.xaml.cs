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
using Kistl.Client.GUI;
using Kistl.Client.Presentables;
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.WPF.CustomControls;
using Kistl.Client.WPF.View.KistlBase;

namespace Kistl.Client.WPF.View
{
    /// <summary>
    /// Interaction logic for EnumSelectionView.xaml
    /// </summary>
    public partial class EnumSelectionView : PropertyEditor, IHasViewModel<EnumerationValueViewModel>
    {
        public EnumSelectionView()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<EnumerationPropertyModel> Members

        public EnumerationValueViewModel ViewModel
        {
            get { return (EnumerationValueViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return cb; }
        }

        protected override void SetHighlightValue(FrameworkElement ctrl, DependencyProperty dpProp, Highlight h, Converter.HighlightConverter converter, TypeConverter finalConverter)
        {
            if(dpProp != BackgroundProperty) base.SetHighlightValue(ctrl, dpProp, h, converter, finalConverter);
        }
    }
}
