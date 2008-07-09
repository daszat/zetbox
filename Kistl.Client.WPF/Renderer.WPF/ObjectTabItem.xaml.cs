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
using System.Windows.Shapes;
using Kistl.GUI;
using System.ComponentModel;

namespace Kistl.GUI.Renderer.WPF
{
    /// <summary>
    /// Interaktionslogik für Window1.xaml
    /// </summary>
    public partial class ObjectTabItem : TabItem, IObjectControl
    {
        public ObjectTabItem()
        {
            InitializeComponent();
        }

        #region IObjectControl Member

        Kistl.API.IDataObject IObjectControl.Value { get; set; }

        public event EventHandler UserSaveRequest;

        public event EventHandler UserDeleteRequest;

        #endregion

        #region IBasicControl Member

        // TODO: useful implementation missing, use DependencyProperties and bind to XAML
        string IBasicControl.ShortLabel { get; set; }
        string IBasicControl.Description { get; set; }
        FieldSize IBasicControl.Size { get; set; }

        #endregion
    }
}
