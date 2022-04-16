using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Zetbox.Client.Presentables.ObjectEditor;
using Zetbox.Client.WPF.Toolkit;

namespace Zetbox.Client.WPF.ObjectEditor
{
    /// <summary>
    /// Interaction logic for SideBarLeft.xaml
    /// </summary>
    public partial class SideBarLeft : UserControl, IHasViewModel<WorkspaceViewModel>, IDragDropTarget, IDragDropSource
    {
        public SideBarLeft()
        {
            InitializeComponent();
        }

        private WpfDragDropHelper _dragDrop;

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            this.lstItems.Loaded += (s, e2) => { _dragDrop = new WpfDragDropHelper(lstItems.FindScrollContentPresenter() ?? this.lstItems, this); };
        }

        public WorkspaceViewModel ViewModel
        {
            get { return (WorkspaceViewModel)WPFHelper.SanitizeDataContext(DataContext); }
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
                return WpfDragDropHelper.ZetboxObjectDataFormatsWithFileDrop;
            }
        }

        Task<bool> IDragDropTarget.OnDrop(string format, object data)
        {
            if (ViewModel == null) return Task.FromResult(false);
            return ViewModel.OnDrop(data);
        }

        object IDragDropSource.GetData()
        {
            return ViewModel.DoDragDrop();
        }
        #endregion
    }
}
