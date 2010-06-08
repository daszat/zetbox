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
using Kistl.Client.Presentables.ModuleEditor;
using QuickGraph;
using Kistl.App.Base;
using GraphSharp.Controls;

namespace Kistl.Client.WPF.View.ModuleEditor
{
    [CLSCompliant(false)]
    public class DataTypeGraphLayout : GraphLayout<DataTypeGraphModel, IEdge<DataTypeGraphModel>, DataTypeGraph> { }

    /// <summary>
    /// Interaction logic for DiagramDisplay.xaml
    /// </summary>
    public partial class DiagramDisplay : UserControl, IHasViewModel<DiagramViewModel>
    {
        public DiagramDisplay()
        {
            InitializeComponent();
        }

        public DiagramViewModel ViewModel
        {
            get { return (DiagramViewModel)DataContext; }
        }
    }
}
