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
using Kistl.Client.Presentables;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for ObjectReferenceEditor.xaml
    /// </summary>
    public partial class ObjectReferenceEditor : PropertyEditor, IHasViewModel<ObjectReferenceModel>
    {
        public ObjectReferenceEditor()
        {
            InitializeComponent();
        }

        #region IHasViewModel<ObjectReferenceModel> Members

        public ObjectReferenceModel ViewModel
        {
            get { return (ObjectReferenceModel)DataContext; }
        }

        #endregion
    }
}
