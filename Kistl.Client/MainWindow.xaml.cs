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

namespace Kistl.Client
{
    /// <summary>
    /// Hauptfenster der KistlApplikation
    /// </summary>
    public partial class MainWindow : Window
    {
        static MainWindow()
        {
            ChangeCenter = new RoutedUICommand("Change Center", "ChangeCenter", typeof(MainWindow));

            CommandManager.RegisterClassCommandBinding(typeof(MainWindow), new CommandBinding(ChangeCenter,
                new ExecutedRoutedEventHandler(ExecuteChangeCenter),
                new CanExecuteRoutedEventHandler(CanExecuteChangeCenter)));
        }

        public MainWindow()
        {
            InitializeComponent();
        }


        private static void ExecuteChangeCenter(object sender, ExecutedRoutedEventArgs e)
        {
            ObjNode theThing = (ObjNode)e.Parameter;

            ((MainWindow)sender).InstanceChangeCenter(theThing);
        }

        private void InstanceChangeCenter(ObjNode thing)
        {
            try
            {
                graph.CenterObject = null;
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
            this.Close();
        }

        /// <summary>
        /// DropDown Liste hat sich geändert -> Objektliste im unteren Bereich aktualisieren
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Kistl.App.Base.ObjectClass objClass = cbObjectTypes.SelectedItem as Kistl.App.Base.ObjectClass;
                if (objClass != null)
                {
                    // Neue Objekttypen setzen & neu Binden
                    // TODO: Man sollte gleich das ObjektClass Objekt übergeben.
                    lst.SourceObjectType = new ObjectType(objClass.GetObject<Kistl.App.Base.Module>("Module").Namespace, objClass.ClassName);
                    lst.Bind();
                }
            }
            catch(Exception ex)
            {
                Helper.HandleError(ex);
            }
        }

        private void lst_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                InstanceChangeCenter(new ObjNode((BaseDataObject)e.AddedItems[0], true));
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
                    Kistl.App.Base.ObjectClassClient client = new Kistl.App.Base.ObjectClassClient();
                    lst.SourceObjectType = client.Type;

                    this.DataContext = Helper.ObjectClasses.Values;
                }
                catch (Exception ex)
                {
                    Helper.HandleError(ex);
                }

                SplashScreen.HideSplashScreen();
            }
            catch (Exception ex)
            {
                Helper.HandleError(ex);
            }
        }
    }

    internal class NodeTemplateSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            ObjNode n = item as ObjNode;
            BaseDataObject obj = n.Item;
            if(n.IsCenter)
                return (DataTemplate)((FrameworkElement)container).FindResource("specialTemplate");
            else
                return (DataTemplate)((FrameworkElement)container).FindResource("nodeTemplate");
        }
    }

    internal class ObjNode : DependencyObject
    {
        public ObjNode(Kistl.API.BaseDataObject obj, bool isCenter)
        {
            IsCenter = isCenter;
            Item = obj;
        }

        public bool IsCenter { get; set; }

        public List<ObjNode> SubItems
        {
            get 
            {
                List<ObjNode> result = new List<ObjNode>();

                try
                {
                    Kistl.App.Base.ObjectClass objClass = Helper.ObjectClasses[Item.Type];
                    List<Kistl.App.Base.BaseProperty> properties = new List<Kistl.App.Base.BaseProperty>();

                    IClientObject client = ClientObjectFactory.GetClientObject(Item.Type);
                    Kistl.App.Base.ObjectClassClient objClassClient = new Kistl.App.Base.ObjectClassClient();

                    while (objClass != null)
                    {
                        properties.AddRange(objClassClient.GetListOfProperties(objClass.ID));
                        objClass = objClass.GetObject<Kistl.App.Base.ObjectClass>("BaseObjectClass");
                    }

                    foreach (Kistl.App.Base.BackReferenceProperty p in properties.OfType<Kistl.App.Base.BackReferenceProperty>())
                    {
                        client.GetListOfGeneric(Item.ID, p.PropertyName).
                            OfType<BaseDataObject>().ToList().ForEach(o => result.Add(new ObjNode(o, false)));
                    }

                    foreach (Kistl.App.Base.ObjectReferenceProperty p in properties.OfType<Kistl.App.Base.ObjectReferenceProperty>())
                    {
                        IClientObject pClient = ClientObjectFactory.GetClientObject(new ObjectType(p.ReferenceObjectClassName));
                        BaseDataObject item = (BaseDataObject)pClient.GetObjectGeneric((int)Item.
                            GetType().GetProperty(p.PropertyName).GetValue(Item, null));
                        if (item != null)
                        {
                            // Kann überraschenderweise null sein
                            result.Add(new ObjNode(item, false));
                        }
                    }
                }
                catch(Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.ToString());
                }

                return result;
            }
        }

        public Kistl.API.BaseDataObject Item
        {
            get { return (Kistl.API.BaseDataObject)GetValue(ItemProperty); }
            set { SetValue(ItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Item.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ItemProperty =
            DependencyProperty.Register("Item", typeof(Kistl.API.BaseDataObject), typeof(ObjNode));
    }
}
