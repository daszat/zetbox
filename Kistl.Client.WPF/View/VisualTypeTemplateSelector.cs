
namespace Kistl.Client.WPF.View
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;

    /// <summary>
    /// A <see cref="DataTemplateSelector"/> to choose the appropriate view for a specified <see cref="ViewModel"/>.
    /// </summary>
    public class VisualTypeTemplateSelector
        : DataTemplateSelector
    {
        /// <summary>
        /// The core method of this class. Chooses the appropriate 
        /// view for a specified <see cref="ViewModel"/> 
        /// according to the specified parameters.
        /// </summary>
        /// <param name="mdl">the model to display</param>
        /// <param name="controlKindClassName">which kind of view to use</param>
        /// <returns>a DataTemplate capable of displaying the specified model</returns>
        public static DataTemplate SelectTemplate(ViewModel mdl, string controlKindClassName)
        {
            if (mdl == null) { throw new ArgumentNullException("mdl"); }

            ViewModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetViewModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching ViewModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            var ckcInterface = new InterfaceType(Type.GetType(controlKindClassName + "," + ApplicationContext.Current.InterfaceAssembly, true));
            return CreateTemplate(pmd.GetViewDescriptor(Toolkit.WPF, ckcInterface));
        }

        private static DataTemplate SelectTemplate(ViewModel mdl, ControlKind controlKind)
        {
            ViewModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetViewModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching ViewModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            return CreateTemplate(pmd.GetViewDescriptor(Toolkit.WPF, controlKind));
        }

        private static DataTemplate SelectDefaultTemplate(ViewModel mdl)
        {
            ViewModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetViewModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching ViewModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            return CreateTemplate(pmd.GetViewDescriptor(Toolkit.WPF));
        }

        private static Dictionary<Type, DataTemplate> templateCache = new Dictionary<Type, DataTemplate>();

        private static DataTemplate CreateTemplate(ViewDescriptor visualDesc)
        {
            if (visualDesc == null)
            {
                return null;
            }

            Logging.Log.DebugFormat("Creating Template with {0}", visualDesc.ToString());
            Type t = visualDesc.ControlRef.AsType(true);
            DataTemplate result;
            lock (templateCache)
            {
                if (!templateCache.ContainsKey(t))
                {
                    result = new DataTemplate();
                    result.VisualTree = new FrameworkElementFactory(t);
                    templateCache[t] = result;
                }
                else
                {
                    result = templateCache[t];
                }
            }
            return result;
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

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return GetNullTemplate(container);
            }

            var rk = RequestedKind ?? GetRequestedKind(container);

            var model = item as ViewModel;
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
                return GetEmptyTemplate(container);
            }
        }

        private static DataTemplate GetEmptyTemplate(DependencyObject container)
        {
            return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
        }

        private static DataTemplate GetNullTemplate(DependencyObject container)
        {
            return (DataTemplate)((FrameworkElement)container).FindResource("nullTemplate");
        }

        public static object GetRequestedKind(DependencyObject obj)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            return (object)obj.GetValue(RequestedKindProperty);
        }

        public static void SetRequestedKind(DependencyObject obj, object value)
        {
            if (obj == null) { throw new ArgumentNullException("obj"); }

            obj.SetValue(RequestedKindProperty, value);
        }

        // default DependencyProperty template
        public static readonly DependencyProperty RequestedKindProperty =
            DependencyProperty.RegisterAttached("RequestedKind", typeof(object), typeof(VisualTypeTemplateSelector), new UIPropertyMetadata());
    }
}
