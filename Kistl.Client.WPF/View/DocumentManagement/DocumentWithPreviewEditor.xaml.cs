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
using Kistl.Client.Presentables.DocumentManagement;

namespace Kistl.Client.WPF.View.DocumentManagement
{
    /// <summary>
    /// Interaction logic for ImageEditor.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class DocumentWithPreviewEditor : PreviewEditor, IHasViewModel<DocumentViewModel>
    {
        public DocumentWithPreviewEditor()
        {
            if (DesignerProperties.GetIsInDesignMode(this)) return;
            InitializeComponent();
        }

        #region IHasViewModel<ImageViewModel> Members

        public new DocumentViewModel ViewModel
        {
            get { return (DocumentViewModel)DataContext; }
        }

        #endregion

        protected override ContentPresenter PreviewControl
        {
            get { return preview; }
        }
    }
}
