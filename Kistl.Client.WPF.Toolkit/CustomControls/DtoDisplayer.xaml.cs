
namespace Kistl.Client.WPF.CustomControls
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
    using Kistl.Client.Presentables.DtoViewModels;

    /// <summary>
    /// Interaction logic for DtoDisplayer.xaml
    /// </summary>
    public partial class DtoDisplayer : UserControl
    {
        public DtoDisplayer()
        {
            InitializeComponent();
        }

        public void Grid_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
                InitGrid(grid, e.NewValue as DtoTableViewModel);
        }

        public void Grid_Initialized(object sender, EventArgs e)
        {
            var grid = sender as Grid;
            if (grid != null)
                InitGrid(grid, grid.DataContext as DtoTableViewModel);
        }

        private static void InitGrid(Grid grid, DtoTableViewModel vm)
        {
            if (vm != null && grid != null)
            {
                grid.RowDefinitions.Clear();
                grid.ColumnDefinitions.Clear();
                foreach (var row in vm.Rows)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                }
                foreach (var col in vm.Columns)
                {
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { SharedSizeGroup = "col" + col.Column.ToString() });
                }

                grid.InvalidateArrange();
                grid.UpdateLayout();
            }
        }
    }
}
