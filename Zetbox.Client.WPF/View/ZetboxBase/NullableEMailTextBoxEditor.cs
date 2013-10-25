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
using Zetbox.Client.GUI;
using Zetbox.Client.Presentables.ValueViewModels;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.View
{
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class NullableEMailTextBoxEditor : PropertyEditor, IHasViewModel<EmailStringValueViewModel>
    {
        static NullableEMailTextBoxEditor()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NullableEMailTextBoxEditor), new FrameworkPropertyMetadata(typeof(NullableEMailTextBoxEditor)));
        }

        public NullableEMailTextBoxEditor()
        {
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            SetupFocusManagement((FrameworkElement)GetTemplateChild("txtNullablePropertyTextBoxView"), () => ViewModel);
        }

        #region IHasViewModel<EmailStringValueViewModel> Members

        public EmailStringValueViewModel ViewModel
        {
            get { return (EmailStringValueViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        #endregion
        protected override FrameworkElement MainControl
        {
            get { return (FrameworkElement)GetTemplateChild("txtNullablePropertyTextBoxView"); }
        }
    }
}
