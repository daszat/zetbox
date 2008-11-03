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

using Kistl.API;
using Kistl.GUI;
using Kistl.App.GUI;

namespace Kistl.GUI.Renderer.WPF
{

    /// <summary>
    /// a non-modifying proxy to simplify PresenterInfo handling in the DB
    /// </summary>
    public class TemplateEditorPresenter : ObjectPresenter { }

    /// <summary>
    /// Interaction logic for TemplateEditor.xaml
    /// </summary>
    public partial class TemplateEditor : ObjectTabItem, IObjectControl
    {
        public TemplateEditor()
        {
            InitializeComponent();
        }

        #region IObjectControl Members

        public Kistl.API.IDataObject Value
        {
            get
            {
                return (Kistl.API.IDataObject)DataContext;
            }
            set
            {
                if (!(value is Template))
                    throw new ArgumentOutOfRangeException("value",
                        String.Format("TemplateEditor.Value can only handle a Template, not '{0}' of type '{1}'",
                            value,
                            value.GetType()));
                DataContext = value;
            }
        }

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
            DependencyProperty.Register("ShortLabel", typeof(string), typeof(TemplateEditor), new UIPropertyMetadata(""));

        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Description.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(TemplateEditor), new UIPropertyMetadata(""));


        public FieldSize Size
        {
            get { return (FieldSize)GetValue(SizeProperty); }
            set { SetValue(SizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Size.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SizeProperty =
            DependencyProperty.Register("Size", typeof(FieldSize), typeof(TemplateEditor), new UIPropertyMetadata(null));

        public IKistlContext Context
        {
            get { return (IKistlContext)GetValue(ContextProperty); }
            set { SetValue(ContextProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Context.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ContextProperty =
            DependencyProperty.Register("Context", typeof(IKistlContext), typeof(TemplateEditor), new UIPropertyMetadata());


        #endregion
    }
}
