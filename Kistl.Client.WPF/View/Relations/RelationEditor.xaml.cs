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
using Kistl.Client.Presentables.Relations;

namespace Kistl.Client.WPF.View.Relations
{
    /// <summary>
    /// Interaction logic for RelationEditor.xaml
    /// </summary>
    public partial class RelationEditor : UserControl, IHasViewModel<RelationViewModel>
    {

        public RelationEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public RelationViewModel ViewModel
        {
            get { return (RelationViewModel)DataContext; }
        }
    }

}
