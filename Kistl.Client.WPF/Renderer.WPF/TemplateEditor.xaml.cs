using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.Client;
using Kistl.GUI;
using Kistl.GUI.DB;
using System.ComponentModel;

namespace Kistl.GUI.Renderer.WPF
{

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
                return ((TemplateView)DataContext).Model;
            }
            set
            {
                if (!(value is Template))
                    throw new ArgumentOutOfRangeException("value",
                        String.Format("TemplateEditor.Value can only handle a Template, not '{0}' of type '{1}'",
                            value,
                            value.GetType()));
                DataContext = new TemplateView((Template)value);
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ObjectClass cls = GuiApplicationContext.Current.Renderer.ChooseObject<ObjectClass>(Context, "Which class to template?");
            if (cls == null) return;

            // HACK: rework TemplateHelper.CreateDefaultTemplate to correctly fill a given Template
            Template tmpl = TemplateHelper.CreateDefaultTemplate(Context, cls.GetDataType());
            Template thisTemplate = (Template)Value;
            thisTemplate.DisplayedTypeAssembly = tmpl.DisplayedTypeAssembly;
            thisTemplate.DisplayedTypeFullName = tmpl.DisplayedTypeFullName;
            thisTemplate.DisplayName = tmpl.DisplayName;
            foreach (var m in tmpl.Menu)
            {
                thisTemplate.Menu.Add(m);
            }
            thisTemplate.VisualTree = tmpl.VisualTree;
            Context.Delete(tmpl);
        }

    }

    /// <summary>
    /// A ViewModel class for <see cref="Kistl.App.GUI.Visual"/>
    /// </summary>
    public class VisualProxy
    {
        public Kistl.App.GUI.Visual Model { get; private set; }
        public ObservableCollection<VisualProxy> Children { get; private set; }

        public VisualProxy(Kistl.App.GUI.Visual v)
        {
            this.Children = new ObservableCollection<VisualProxy>();
            this.Model = v;
            this.Model.PropertyChanged += Visual_PropertyChanged;
            UpdateChildrenCollection();
        }

        void Visual_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Children")
                UpdateChildrenCollection();
        }

        private void UpdateChildrenCollection()
        {
            this.Children.Clear();
            foreach (var v in this.Model.Children)
            {
                this.Children.Add(new VisualProxy(v));
            }
        }
    }

    public class TemplateView
    {
        public Template Model { get; private set; }
        public ObservableCollection<VisualProxy> VisualTree { get; private set; }

        public TemplateView(Template t)
        {
            this.VisualTree = new ObservableCollection<VisualProxy>();
            this.Model = t;
            this.Model.PropertyChanged += new PropertyChangedEventHandler(Model_PropertyChanged);
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VisualTree")
            {
                this.VisualTree.Clear();
                if (this.Model.VisualTree != null)
                    this.VisualTree.Add(new VisualProxy(this.Model.VisualTree));
            }
        }
    }

    /// <summary>
    /// a non-modifying proxy to simplify PresenterInfo handling in the DB
    /// </summary>
    public class TemplateEditorPresenter : ObjectPresenter { }

}
