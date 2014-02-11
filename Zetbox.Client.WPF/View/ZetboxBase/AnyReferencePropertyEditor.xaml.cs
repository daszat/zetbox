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
    public partial class AnyReferencePropertyEditor : PropertyEditor, IHasViewModel<AnyReferencePropertyViewModel>, IDragDropTarget, IDragDropSource
    {
        public AnyReferencePropertyEditor()
        {
            InitializeComponent();
            _dragDrop = new WpfDragDropHelper(this);
        }

        private WpfDragDropHelper _dragDrop;

        public AnyReferencePropertyViewModel ViewModel
        {
            get { return (AnyReferencePropertyViewModel)WPFHelper.SanitizeDataContext(DataContext); }
        }

        protected override FrameworkElement MainControl
        {
            get { return null; }
        }

        #region IDragDrop*
        bool IDragDropTarget.CanDrop
        {
            get { return ViewModel != null && ViewModel.CanDrop; }
        }

        string[] IDragDropTarget.AcceptableDataFormats
        {
            get
            {
                return WpfDragDropHelper.ZetboxObjectDataFormats;
            }
        }

        bool IDragDropTarget.OnDrop(string format, object data)
        {
            if (ViewModel == null) return false;
            return ViewModel.OnDrop(data);
        }

        object IDragDropSource.GetData()
        {
            return ViewModel.DoDragDrop();
        }
        #endregion
    }
}
