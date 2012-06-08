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
using Zetbox.Client.Presentables.ValueViewModels;

namespace Zetbox.Client.WPF.View.GridCells
{
    /// <summary>
    /// Interaction logic for ReferenceEditor.xaml
    /// </summary>
    public partial class ReferenceEditor : UserControl, IHasViewModel<ObjectReferenceViewModel>
    {
        public ReferenceEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<ObjectReferenceModel> Members

        public ObjectReferenceViewModel ViewModel
        {
            get { return (ObjectReferenceViewModel)DataContext; }
        }

        #endregion
    }
}
