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

using Zetbox.Client.GUI;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.Relations;

namespace Zetbox.Client.WPF.View.Relations
{
    /// <summary>
    /// Interaction logic for RelationEndEditor.xaml
    /// </summary>
    public partial class RelationEndEditor : UserControl, IHasViewModel<RelationEndViewModel>
    {
        public RelationEndEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        public RelationEndViewModel ViewModel
        {
            get { return (RelationEndViewModel)DataContext; }
        }
    }

}
