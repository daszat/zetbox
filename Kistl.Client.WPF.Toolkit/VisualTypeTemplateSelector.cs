#define DONT_CACHE_TEMPLATE // Avoid caching, templates will be resused. At least the Combobox gets confused.

namespace Kistl.Client.WPF.Toolkit
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
        public delegate VisualTypeTemplateSelector Factory(object requestedKind);

        private readonly IFrozenContext _frozenCtx;
        private readonly object _requestedKind;

        private static DataTemplate SelectTemplate(ViewModel mdl, string controlKindName, IFrozenContext frozenCtx)
        {
            var ck = frozenCtx.GetQuery<ControlKind>().SingleOrDefault(c => c.Name == controlKindName);
            return SelectTemplate(mdl, ck, frozenCtx);
        }

        private static TypeRef GetTypeRef(ViewModel mdl, IFrozenContext frozenCtx)
        {
            var tr = mdl.GetType().ToRef(frozenCtx);
            if (tr == null)
            {
                var mdlType = mdl.GetType();
                if (mdlType.IsGenericType)
                {
                    Logging.Log.ErrorFormat("Unable to resolve TypeRef of given ViewModel '{0}'. You have to manually create a generic TypeRef.", mdlType);
                }
                else
                {
                    Logging.Log.ErrorFormat("Unable to resolve TypeRef of given ViewModel '{0}'. Regenerate Assembly Refs.", mdlType);
                }
                return null;
            }
            return tr;
        }

        private static DataTemplate SelectTemplate(ViewModel mdl, ControlKind controlKind, IFrozenContext frozenCtx)
        {
            var tr = GetTypeRef(mdl, frozenCtx);
            if (tr == null) return null;

            ViewModelDescriptor pmd = tr.GetViewModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching ViewModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            return CreateTemplate(pmd.GetViewDescriptor(Toolkit.WPF, controlKind));
        }

        private static DataTemplate SelectDefaultTemplate(ViewModel mdl, IFrozenContext frozenCtx)
        {
            var tr = GetTypeRef(mdl, frozenCtx);
            if (tr == null) return null;

            ViewModelDescriptor pmd = tr.GetViewModelDescriptor();
            if (pmd == null)
            {
                Logging.Log.ErrorFormat("No matching ViewModelDescriptor found for {0}", mdl.GetType());
                return null;
            }

            return CreateTemplate(pmd.GetViewDescriptor(Toolkit.WPF));
        }

#if !DONT_CACHE_TEMPLATE
        private static Dictionary<Type, DataTemplate> templateCache = new Dictionary<Type, DataTemplate>();
#endif

        private static DataTemplate CreateTemplate(ViewDescriptor visualDesc)
        {
            if (visualDesc == null)
            {
                return null;
            }

            if (Logging.Log.IsDebugEnabled) { Logging.Log.DebugFormat("Creating Template with {0}", visualDesc.ToString()); }
            Type t = visualDesc.ControlRef.AsType(true);
            DataTemplate result;
#if !DONT_CACHE_TEMPLATE
            lock (templateCache)
            {
                if (!templateCache.ContainsKey(t))
                {
#endif
                    result = new DataTemplate();
                    result.VisualTree = new FrameworkElementFactory(t);
#if !DONT_CACHE_TEMPLATE
                    templateCache[t] = result;
                }
                else
                {
                    result = templateCache[t];
                }
            }
#endif
                    
            return result;
        }

        /// <summary>
        /// Initializes a new instance of the VisualTypeTemplateSelector class.
        /// </summary>
        public VisualTypeTemplateSelector(object requestedKind, IFrozenContext frozenCtx)
        {
            this._requestedKind = requestedKind;
            this._frozenCtx = frozenCtx;
        }

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return GetNullTemplate(container);
            }

            var rk = _requestedKind ?? GetRequestedKind(container);

            var model = item as ViewModel;
            DataTemplate result = null;
            if (model != null)
            {
                if (rk is ControlKind)
                {
                	if (Logging.Log.IsDebugEnabled) { Logging.Log.DebugFormat("Searching '{0}' Template for {1}", rk.ToString(), model.GetType().FullName); }
                    result = SelectTemplate(model, rk as ControlKind, _frozenCtx);
                }
                else if (rk is String)
                {
                	if (Logging.Log.IsDebugEnabled) { Logging.Log.DebugFormat("Searching '{0}' Template for {1}", rk, model.GetType().FullName); }
                    result = SelectTemplate(model, rk as String, _frozenCtx);
                }
                else if (rk == null && model.RequestedKind != null)
                {
                	if (Logging.Log.IsDebugEnabled) { Logging.Log.DebugFormat("Searching '{0}' Template for {1}", model.RequestedKind.ToString(), model.GetType().FullName); }
                    result = SelectTemplate(model, model.RequestedKind, _frozenCtx);
                }
                else if (rk == null)
                {
                	if (Logging.Log.IsDebugEnabled) { Logging.Log.DebugFormat("Searching default Template for {0}", model.GetType().FullName); }
                    result = SelectDefaultTemplate(model, _frozenCtx);
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
