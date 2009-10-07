
namespace Kistl.Client.WPF.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using System.Diagnostics;
    using Kistl.API.Utils;

    /// <summary>
    /// A <see cref="DataTemplateSelector"/> to choose the appropriate view for a specified <see cref="PresentableModel"/>.
    /// </summary>
    public class VisualTypeTemplateSelector
        : DataTemplateSelector
    {
        /// <summary>
        /// The core method of this class. Chooses the appropriate 
        /// view for a specified <see cref="PresentableModel"/> 
        /// according to the specified parameters.
        /// </summary>
        /// <param name="mdl">the model to display</param>
        /// <param name="controlKindClassName">which kind of view to use</param>
        /// <returns>a DataTemplate capable of displaying the specified model</returns>
        public static DataTemplate SelectTemplate(PresentableModel mdl, string controlKindClassName)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetPresentableModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching PresentableModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            return CreateTemplate(LookupSecondaryViewDescriptor(pmd, controlKindClassName));
        }

        private static DataTemplate SelectTemplate(PresentableModel mdl, ControlKind controlKind)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetPresentableModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching PresentableModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            return CreateTemplate(pmd.GetViewDescriptor(Toolkit.WPF, controlKind));
        }

        private static DataTemplate SelectDefaultTemplate(PresentableModel mdl)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetPresentableModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching PresentableModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            return CreateTemplate(pmd.GetDefaultViewDescriptor(Toolkit.WPF));
        }

        private static DataTemplate CreateTemplate(ViewDescriptor visualDesc)
        {
            if (visualDesc == null)
            {
                return null;
            }

            Logging.Log.DebugFormat("Creating Template with {0}", visualDesc.ToString());
            // TODO: cache already generated templates to reduce memory pressure
            DataTemplate result = new DataTemplate();
            result.VisualTree = new FrameworkElementFactory(visualDesc.ControlRef.AsType(true));
            return result;
        }

        private static ViewDescriptor LookupSecondaryViewDescriptor(PresentableModelDescriptor pmd, string controlKindClassName)
        {
            ViewDescriptor visualDesc;
            if (String.IsNullOrEmpty(controlKindClassName))
            {
                visualDesc = pmd.GetDefaultViewDescriptor(Toolkit.WPF);
            }
            else
            {
                var ckcInterface = new InterfaceType(Type.GetType(controlKindClassName + "," + GuiApplicationContext.Current.InterfaceAssembly, true));
                var defaultKind = pmd.GetDefaultKind();
                if (defaultKind != null && defaultKind.GetInterfaceType() == ckcInterface)
                {
                    visualDesc = pmd.GetDefaultViewDescriptor(Toolkit.WPF);
                }
                else
                {
                    ControlKind controlKind = pmd.SecondaryControlKinds.Where(ck => ck.GetInterfaceType() == ckcInterface).SingleOrDefault();
                    if (controlKind == null && pmd.PresentableModelRef.Parent != null)
                    {
                        var parentDescriptor = pmd.PresentableModelRef.Parent.GetPresentableModelDescriptor();
                        if (parentDescriptor != null)
                        {
                            // recursively iterate up the inheritance tree
                            visualDesc = LookupSecondaryViewDescriptor(parentDescriptor, controlKindClassName);
                        }
                        else
                        {
                            Logging.Log.WarnFormat("Couldn't find matching controlKind: '{0}'", controlKindClassName);
                            visualDesc = null;
                        }
                    }
                    else
                    {
                        visualDesc = pmd.GetViewDescriptor(Toolkit.WPF, controlKind);
                    }
                }
            }
            return visualDesc;
        }

        /// <summary>
        /// Initializes a new instance of the VisualTypeTemplateSelector class.
        /// </summary>
        public VisualTypeTemplateSelector()
        {
        }

        /// <summary>
        /// Gets or sets the kind of view to use. To select the default view, leave this property at its default null value.
        /// </summary>
        public object RequestedKind
        {
            get;
            set;
        }



        //public object RequestedKind
        //{
        //    get { return (object)GetValue(RequestedKindProperty); }
        //    set { SetValue(RequestedKindProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for RequestedKind.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty RequestedKindProperty =
        //    DependencyProperty.Register("RequestedKind", typeof(object), typeof(VisualTypeTemplateSelector), new UIPropertyMetadata());



        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return (DataTemplate)((FrameworkElement)container).FindResource("nullTemplate");
            }

            var rk = RequestedKind ?? GetRequestedKind(container);

            var model = item as PresentableModel;
            DataTemplate result = null;
            if (model != null)
            {
                if (rk is ControlKind)
                {
                    Logging.Log.DebugFormat("Searching '{0}' Template for {1}", rk.GetType().FullName, model.GetType().FullName);
                    result = VisualTypeTemplateSelector.SelectTemplate(model, rk as ControlKind);
                }
                else if (rk is String)
                {
                    Logging.Log.DebugFormat("Searching '{0}' Template for {1}", rk, model.GetType().FullName);
                    result = VisualTypeTemplateSelector.SelectTemplate(model, rk as String);
                }
                else if (rk == null)
                {
                    Logging.Log.DebugFormat("Searching default Template for {0}", model.GetType().FullName);
                    result = VisualTypeTemplateSelector.SelectDefaultTemplate(model);
                }
                else
                {
                    result = null;
                    Logging.Log.ErrorFormat("RequestedKind of type '{0}' is neither string nor ControlKind", rk.GetType().FullName);
                }
            }

            if (result != null)
            {
                return result;
            }
            else
            {
                Logging.Log.WarnFormat("No '{0}' DataTemplate found for {1}", rk == null ? "(default)" : (rk is string ? rk : rk.GetType().FullName), item);
                return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
            }
        }



        public static object GetRequestedKind(DependencyObject obj)
        {
            return (object)obj.GetValue(RequestedKindProperty);
        }

        public static void SetRequestedKind(DependencyObject obj, object value)
        {
            obj.SetValue(RequestedKindProperty, value);
        }

        // Using a DependencyProperty as the backing store for RequestedKind.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RequestedKindProperty =
            DependencyProperty.RegisterAttached("RequestedKind", typeof(object), typeof(VisualTypeTemplateSelector), new UIPropertyMetadata());


    }
}
