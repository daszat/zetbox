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
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.View
{

    public class NullablePropertyTextBoxView : PropertyEditor, IHasViewModel<BaseValueViewModel>, IHasViewModel<IFormattedValueViewModel>
    {
        static NullablePropertyTextBoxView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NullablePropertyTextBoxView), new FrameworkPropertyMetadata(typeof(NullablePropertyTextBoxView)));
        }

        public NullablePropertyTextBoxView()
        {            
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetupFocusManagement((FrameworkElement)GetTemplateChild("txtNullablePropertyTextBoxView"), () => ViewModel);
        }

        #region IHasViewModel<BaseValueViewModel> Members

        public BaseValueViewModel ViewModel
        {
            get { return (BaseValueViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion

        #region IHasViewModel<IFormattedValueViewModel> Members

        IFormattedValueViewModel IHasViewModel<IFormattedValueViewModel>.ViewModel
        {
            get { return (IFormattedValueViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return (FrameworkElement)GetTemplateChild("txtNullablePropertyTextBoxView"); }
        }
    }
}
