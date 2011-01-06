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
using Kistl.Client.Presentables.ValueViewModels;
using Kistl.Client.Models;

namespace Kistl.Client.WPF.View.KistlBase
{
    /// <summary>
    /// Interaction logic for ObjectReferenceEditor.xaml
    /// </summary>
    [ViewDescriptor(Kistl.App.GUI.Toolkit.WPF)]
    public partial class ObjectReferenceDropdownEditor : PropertyEditor, IHasViewModel<ObjectReferenceViewModel>
    {
        public ObjectReferenceDropdownEditor()
        {
            InitializeComponent();
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            //if (e.Property == FrameworkElement.DataContextProperty)
            //{
            //    RefreshGridView();
            //}
        }

        #region IHasViewModel<ObjectReferenceViewModel> Members

        public ObjectReferenceViewModel ViewModel
        {
            get { return (ObjectReferenceViewModel)DataContext; }
        }

        #endregion

        protected override FrameworkElement MainControl
        {
            get { return cbValue; }
        }
    }
}
