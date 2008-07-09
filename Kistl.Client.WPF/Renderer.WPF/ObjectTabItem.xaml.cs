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
        public string ShortLabel
        {
            get { return (string)GetValue(ShortLabelProperty); }
            set { SetValue(ShortLabelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ShortLabel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ShortLabelProperty =
            DependencyProperty.Register("ShortLabel", typeof(string), typeof(ObjectTabItem), new UIPropertyMetadata(""));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(ObjectTabItem), new UIPropertyMetadata(""));


        public FieldSize Size
        {
            get { return (FieldSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(FieldSize), typeof(ObjectTabItem), new UIPropertyMetadata(null));

        #endregion
    }
}
