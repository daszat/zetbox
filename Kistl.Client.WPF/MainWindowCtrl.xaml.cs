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
using System.Xml.Serialization;
using Kistl.API;
using System.ComponentModel;
using Kistl.API.Client;
using System.Collections;

namespace Kistl.Client.WPF
{
    /// <summary>
    /// Interaction logic for MainWindowCtrl.xaml
    /// </summary>
    public partial class MainWindowCtrl : UserControl
    {
        static MainWindowCtrl()
        {
            ChangeCenter = new RoutedUICommand("Change Center", "ChangeCenter", typeof(MainWindowCtrl));

            CommandManager.RegisterClassCommandBinding(typeof(MainWindowCtrl), new CommandBinding(ChangeCenter,
                new ExecutedRoutedEventHandler(ExecuteChangeCenter),
                new CanExecuteRoutedEventHandler(CanExecuteChangeCenter)));
        }

        public MainWindowCtrl()
        {
            InitializeComponent();
        }


        private static void ExecuteChangeCenter(object sender, ExecutedRoutedEventArgs e)
        {
            ObjNode theThing = (ObjNode)e.Parameter;

            ((MainWindowCtrl)sender).InstanceChangeCenter(theThing);
        }

        private void InstanceChangeCenter(ObjNode thing)
        {
            try
            {
                graph.CenterObject = thing;
            }
            catch
            {
            }
        }

        private static void CanExecuteChangeCenter(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        public static readonly RoutedUICommand ChangeCenter;

        /// <summary>
        /// Und auf wieder sehen!
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menu_Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        /// <summary>
        /// DropDown Liste hat sich geändert -> Objektliste im unteren Bereich aktualisieren
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // TODO!
            MessageBox.Show("not yet implemented");
#if false
            try
            {
                Kistl.App.Base.ObjectClass objClass = cbObjectTypes.SelectedItem as Kistl.App.Base.ObjectClass;
                if (objClass != null)
                {
                    // Neue Objekttypen setzen & neu Binden
                    // TODO: Man sollte gleich das ObjektClass Objekt übergeben.
                    lst.SourceObjectType = new ObjectType(objClass.Module.Namespace, objClass.ClassName);
                    lst.Bind();
                }
            }
            catch(Exception ex)
            {
                Helper.HandleError(ex);
            }
#endif
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                if (e.AddedItems.Count > 0)
                {
                    InstanceChangeCenter(new ObjNode((Kistl.API.IDataObject)e.AddedItems[0], true));
                }
            }
        }

        private void DesktopTreeView_SelectionChanged(object sender, RoutedPropertyChangedEventArgs<Kistl.API.IDataObject> e)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                if (e.NewValue != null)
                {
                    InstanceChangeCenter(new ObjNode(e.NewValue, true));
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                // DesingMode? -> raus
                if (DesignerProperties.GetIsInDesignMode(this)) return;

                SplashScreen.SetInfo("Loading Objects");
                try
                {
                    // Hole eine Liste aller ObjectClasses Objekte & zeige sie in der DropDown an
                    // lst.SourceObjectType = new ObjectType(typeof(Kistl.App.Base.ObjectClass));
                    // this.DataContext = Helper.ObjectClasses.Values;
                }
                catch (Exception ex)
                {
                    ClientHelper.HandleError(ex);
                }

                SplashScreen.HideSplashScreen();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
        }

        private void menu_Generate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SplashScreen.ShowSplashScreen("Generating Objects and Database", "This may take several seconds", 1);
                Proxy.Current.Generate();
            }
            catch (Exception ex)
            {
                ClientHelper.HandleError(ex);
            }
            SplashScreen.HideSplashScreen();
        }

    }

    internal class NodeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ObjNode n = item as ObjNode;
            Kistl.API.IDataObject obj = n.Item;
            if (n.IsCenter)
                return (DataTemplate)((FrameworkElement)container).FindResource("specialTemplate");
            else
                return (DataTemplate)((FrameworkElement)container).FindResource("nodeTemplate");
        }
    }

    internal class ObjNode : DependencyObject
    {
        public ObjNode(Kistl.API.IDataObject obj, bool isCenter)
        {
            IsCenter = isCenter;
            Item = obj;
        }

        public bool IsCenter { get; set; }

        [System.Diagnostics.DebuggerHidden]
        [System.ComponentModel.Browsable(false)]
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public List<ObjNode> SubItems
        {
            get
            {
                List<ObjNode> result = new List<ObjNode>();

                try
                {
                    Kistl.App.Base.ObjectClass objClass = ClientHelper.ObjectClasses[Item.GetType()];
                    List<Kistl.App.Base.BaseProperty> properties = new List<Kistl.App.Base.BaseProperty>();

                    while (objClass != null)
                    {
                        properties.AddRange(objClass.Properties);
                        objClass = objClass.BaseObjectClass;
                    }

                    foreach (Kistl.App.Base.BackReferenceProperty p in properties.OfType<Kistl.App.Base.BackReferenceProperty>())
                    {
                        Item.GetPropertyValue<IEnumerable>(p.PropertyName)
                            .ForEach<Kistl.API.IDataObject>(o => result.Add(new ObjNode(o, false)));
                    }

                    foreach (Kistl.App.Base.ObjectReferenceProperty p in properties.OfType<Kistl.App.Base.ObjectReferenceProperty>().Where(p => !p.IsList))
                    {
                        Kistl.API.IDataObject item = Item.GetPropertyValue<Kistl.API.IDataObject>(p.PropertyName);
                        if (item != null)
                        {
                            // Kann überraschenderweise null sein
                            result.Add(new ObjNode(item, false));
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

                return result;
            }
        }

        public Kistl.API.IDataObject Item
        {
            get { return (Kistl.API.IDataObject)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(Kistl.API.IDataObject), typeof(ObjNode));
    }
}
