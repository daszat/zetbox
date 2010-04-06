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
using Kistl.Client.Presentables.ModuleEditor;
using Kistl.Client.GUI;

namespace Kistl.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for ObjectClassInstanceList.xaml
    /// </summary>
    public partial class ObjectClassInstanceList : UserControl, IHasViewModel<ObjectClassInstanceListViewModel>
    {
        public ObjectClassInstanceList()
        {
            InitializeComponent();
        }

        public ObjectClassInstanceListViewModel ViewModel
        {
            get { return (ObjectClassInstanceListViewModel)DataContext; }
        }
    }
}
