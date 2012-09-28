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
using System.ComponentModel;
using Zetbox.Client.Presentables.ModuleEditor;
using GraphSharp.Controls;
using QuickGraph;

namespace Zetbox.Client.WPF.View.ModuleEditor
{
    /// <summary>
    /// Interaction logic for DiagramDisplay.xaml
    /// </summary>
    public partial class DiagramDisplay : UserControl, IHasViewModel<DiagramViewModel>
    {
        public DiagramDisplay()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public DiagramViewModel ViewModel
        {
            get { return (DiagramViewModel)DataContext; }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            // Simple WPF Printing -> cant be a command
            PrintDialog printDlg = new System.Windows.Controls.PrintDialog();
            if (printDlg.ShowDialog() == true)
            {
                graphLayout.Measure(new Size(printDlg.PrintableAreaWidth, printDlg.PrintableAreaHeight));
                graphLayout.Arrange(new Rect(new Point(0, 0), graphLayout.DesiredSize));
                printDlg.PrintVisual(graphLayout, "Some Datatypes from " + ViewModel.Module.Name);
            }
        }
    }

    [CLSCompliant(false)]
    public class DataTypeGraphLayout : GraphLayout<DataTypeGraphModel, IEdge<DataTypeGraphModel>, DataTypeGraph> { }
}