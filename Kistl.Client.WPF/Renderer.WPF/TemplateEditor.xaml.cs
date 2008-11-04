using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.Client;
using Kistl.GUI;
using Kistl.GUI.DB;

namespace Kistl.GUI.Renderer.WPF
{

    /// <summary>
    /// Interaction logic for TemplateEditor.xaml
    /// </summary>
    public partial class TemplateEditor : PropertyControl, IReferenceControl
    {
        public TemplateEditor()
        {
            this.ValueView = new ObservableCollection<VisualView>();
            InitializeComponent();
        }

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


        #region IValueControl<Template> Members

        public Kistl.App.GUI.Visual Value
        {
            get { return (Kistl.App.GUI.Visual)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Value.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(Kistl.App.GUI.Visual), typeof(TemplateEditor), new UIPropertyMetadata(null, ValueChangedHandler));

        private static void ValueChangedHandler(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var editor = (TemplateEditor)sender;

            editor.ValueView.Clear();
            if (editor.Value != null)
                editor.ValueView.Add(new VisualView(editor.Value));
        }

        public ObservableCollection<VisualView> ValueView
        {
            get { return (ObservableCollection<VisualView>)GetValue(ValueViewProperty); }
            set { SetValue(ValueViewProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ValueView.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ValueViewProperty =
            DependencyProperty.Register("VisualView", typeof(ObservableCollection<VisualView>), typeof(TemplateEditor), new UIPropertyMetadata(null));

        #endregion

        #region IReferenceControl Members

        public Type ObjectType { get; set; }

        public IList<Kistl.API.IDataObject> ItemsSource { get; set; }

        #endregion

        #region IValueControl<IDataObject> Members

        Kistl.API.IDataObject IValueControl<Kistl.API.IDataObject>.Value
        {
            get { return this.Value; }
            set { this.Value = (Kistl.App.GUI.Visual)value; }
        }

        public event EventHandler UserInput;
        protected virtual void OnUserInput()
        {
            if (this.UserInput != null)
            {
                this.UserInput(this, EventArgs.Empty);
            }
        }

        #endregion

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var obj = ((VisualView)this.VisualTree.SelectedItem).Model;

            // TODO: should come from DataType
            var template = obj.FindTemplate(TemplateUsage.EditControl);
            // the Meat
            this.SelectedVisual.Content = GuiApplicationContext.Current.Renderer.CreateControl(obj, template.VisualTree);
        }
    }

    /// <summary>
    /// A ViewModel class for <see cref="Kistl.App.GUI.Visual"/>
    /// </summary>
    public class VisualView : INotifyPropertyChanged
    {
        public Kistl.App.GUI.Visual Model { get; private set; }
        public string ModelToString { get { return Model.ToString(); } }
        public ObservableCollection<VisualView> Children { get; private set; }

        public VisualView(Kistl.App.GUI.Visual v)
        {
            if (v == null)
                throw new ArgumentNullException("v");

            this.Children = new ObservableCollection<VisualView>();
            this.Model = v;
            this.Model.PropertyChanged += Visual_PropertyChanged;
            UpdateChildrenCollection();
        }

        void Visual_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Children")
                UpdateChildrenCollection();

            OnPropertyChanged("ModelToString");
        }

        private void UpdateChildrenCollection()
        {
            this.Children.Clear();
            foreach (var v in this.Model.Children)
            {
                this.Children.Add(new VisualView(v));
            }
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }

    public class TemplateView
    {
        public Template Model { get; private set; }
        public ObservableCollection<VisualView> VisualTree { get; private set; }

        public TemplateView(Template t)
        {
            this.VisualTree = new ObservableCollection<VisualView>();
            this.Model = t;
            this.Model.PropertyChanged += new PropertyChangedEventHandler(Model_PropertyChanged);
        }

        void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "VisualTree")
            {
                this.VisualTree.Clear();
                if (this.Model.VisualTree != null)
                    this.VisualTree.Add(new VisualView(this.Model.VisualTree));
            }
        }
    }

    /// <summary>
    /// a non-modifying proxy to simplify PresenterInfo handling in the DB
    /// </summary>
    public class TemplateEditorPresenter : ObjectReferencePresenter<Kistl.App.GUI.Visual> { }

}
