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
using Zetbox.Client.Presentables.ZetboxBase;
using Zetbox.Client.WPF.CustomControls;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.View.ZetboxBase
{
    /// <summary>
    /// Interaction logic for AnyReferencePropertyEditor.xaml
    /// </summary>
    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public partial class TagPropertyEditor : PropertyEditor, IHasViewModel<TagPropertyEditorViewModel>
    {
        public TagPropertyEditor()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(TagPropertyEditor_Loaded);
        }

        void TagPropertyEditor_LostFocus(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
        }

        void TagPropertyEditor_Loaded(object sender, RoutedEventArgs args)
        {
            Window w = Window.GetWindow(btnDropDown);
            if (w != null)
            {
                w.LocationChanged += (s,e) =>
                {
                    var offset = myPopup.HorizontalOffset;
                    myPopup.HorizontalOffset = offset + 1;
                    myPopup.HorizontalOffset = offset;
                };
                w.Deactivated += (s, e) =>
                {
                    myPopup.IsOpen = false;
                };
            }
        }

        public TagPropertyEditorViewModel ViewModel
        {
            get { return (TagPropertyEditorViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        protected override FrameworkElement MainControl
        {
            get { return txt; }
        }

        private void txt_GotFocus(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = true;
        }
        
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            myPopup.IsOpen = false;
        }
    }
}
