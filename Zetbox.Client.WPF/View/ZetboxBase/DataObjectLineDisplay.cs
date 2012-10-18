
namespace Zetbox.Client.WPF.View.ZetboxBase
{
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
    using Zetbox.Client.WPF.Toolkit;

    [ViewDescriptor(Zetbox.App.GUI.Toolkit.WPF)]
    public class DataObjectLineDisplay : DockPanel, IHasViewModel<DataObjectViewModel>
    {
        public DataObjectLineDisplay()
        {
            var img = new Image() { Width = 14, Height = 14 };
            BindingOperations.SetBinding(img, Image.SourceProperty, new Binding() { Converter = (IValueConverter)FindResource("IconConverter") });
            Children.Add(img);

            var txt = new TextBlock() { Margin = new Thickness(3, 0, 0, 0) };
            BindingOperations.SetBinding(txt, TextBlock.TextProperty, new Binding("ObjectState") { Converter = (IValueConverter)FindResource("ObjectStateToTextConverter") });
            BindingOperations.SetBinding(txt, TextBlock.VisibilityProperty, new Binding("ObjectState") { Converter = (IValueConverter)FindResource("ObjectStateToTextVisibilityConverter") });
            Children.Add(txt);

            txt = new TextBlock() { Margin = new Thickness(3, 0, 0, 0) };
            BindingOperations.SetBinding(txt, TextBlock.TextProperty, new Binding("Name"));
            Children.Add(txt);
        }

        public DataObjectViewModel ViewModel
        {
            get { return (DataObjectViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }
    }
}
