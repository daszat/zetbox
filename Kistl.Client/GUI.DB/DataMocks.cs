using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.API.Client;
using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.Client.Presentables;

namespace Kistl.Client.GUI.DB
{

    public static class TypeRefExtensions
    {
        public static object Create(this TypeRef t, params object[] parameter)
        {
            return Activator.CreateInstance(t.AsType(true), parameter);
        }

    }

    public class ViewDescriptor
    {
        public ViewDescriptor(TypeRef view, Toolkit tk, TypeRef layout)
        {
            ViewRef = view;
            Toolkit = tk;
            LayoutRef = layout;
        }

        public TypeRef ViewRef { get; private set; }
        public Toolkit Toolkit { get; private set; }
        public TypeRef LayoutRef { get; private set; }

        public override string ToString()
        {
            return String.Format("{0}: Display layout {1} with {2}", Toolkit, LayoutRef.AsType(true).Name, ViewRef.AsType(true).Name);
        }
    }

    public abstract class Layout
    {
        /// <summary>
        /// Which Type can be layouted this way
        /// </summary>
        public virtual Type SourceModelType { get; set; }
    }

    /// <summary>
    /// doesn't contain additional layout information
    /// </summary>
    public class StaticLayout : Layout
    {
    }

    /// <summary>
    /// layout a workspace
    /// </summary>
    public class WorkspaceLayout : Layout
    {
    }

    /// <summary>
    /// layout a nullable value as text
    /// </summary>
    public class SimpleNullableValueLayout<TValue> : Layout
    {
        public bool AllowNullInput { get; set; }
    }

    /// <summary>
    /// layout a nullable enum value
    /// </summary>
    public class SimpleEnumValueLayout : Layout
    {
        public bool AllowNullInput { get; set; }
    }

    /// <summary>
    /// how to layout a text value
    /// </summary>
    public class TextValueLayout : SimpleNullableValueLayout<string>
    {
        public bool IsMultiline { get; set; }
    }

    /// <summary>
    /// how to layout a selection from multiple text values
    /// </summary>
    public class TextValueSelectionLayout : TextValueLayout
    {
    }

    public class ListValueLayout : Layout { }

    /// <summary>
    /// how to layout a data object in a single "line"
    /// </summary>
    public class DataObjectLineLayout : Layout
    {
    }

    /// <summary>
    /// how to layout a data object with all info
    /// </summary>
    public class DataObjectFullLayout : Layout
    {
    }

    public class DataObjectListLayout : Layout
    {
    }

    public class DataObjectReferenceLayout : Layout
    {
    }

    /// <summary>
    /// how to layout a data object
    /// </summary>
    public class SelectionTaskLayout : Layout
    {
    }

    public class ActionLayout : Layout
    {
    }

    public class DebuggerLayout : Layout
    {
    }

    public static class DataMocks
    {

        private static Dictionary<Type, Layout> _defaultLayoutsCache;
        public static Dictionary<Type, Layout> DefaultLayouts
        {
            get
            {
                if (_defaultLayoutsCache == null)
                {
                    _defaultLayoutsCache = new Dictionary<Type, Layout>();

                    AddLayoutCacheEntry(new SimpleNullableValueLayout<Boolean>() { SourceModelType = typeof(NullableValuePropertyModel<Boolean>), AllowNullInput = true });
                    AddLayoutCacheEntry(new SimpleNullableValueLayout<DateTime>() { SourceModelType = typeof(NullableValuePropertyModel<DateTime>), AllowNullInput = true });
                    AddLayoutCacheEntry(new SimpleNullableValueLayout<Double>() { SourceModelType = typeof(NullableValuePropertyModel<Double>), AllowNullInput = true });
                    AddLayoutCacheEntry(new SimpleNullableValueLayout<int>() { SourceModelType = typeof(NullableValuePropertyModel<int>), AllowNullInput = true });

                    AddLayoutCacheEntry(new SimpleNullableValueLayout<Boolean>() { SourceModelType = typeof(NullableResultModel<Boolean>), AllowNullInput = true });
                    AddLayoutCacheEntry(new SimpleNullableValueLayout<DateTime>() { SourceModelType = typeof(NullableResultModel<DateTime>), AllowNullInput = true });
                    AddLayoutCacheEntry(new SimpleNullableValueLayout<Double>() { SourceModelType = typeof(NullableResultModel<Double>), AllowNullInput = true });
                    AddLayoutCacheEntry(new SimpleNullableValueLayout<int>() { SourceModelType = typeof(NullableResultModel<int>), AllowNullInput = true });

                    AddLayoutCacheEntry(new DataObjectFullLayout() { SourceModelType = typeof(DataObjectModel) });
                    AddLayoutCacheEntry(new DataObjectListLayout() { SourceModelType = typeof(ObjectListModel) });
                    AddLayoutCacheEntry(new DataObjectReferenceLayout() { SourceModelType = typeof(ObjectReferenceModel) });
                    AddLayoutCacheEntry(new DataObjectReferenceLayout() { SourceModelType = typeof(ObjectResultModel<IDataObject>) });

                    AddLayoutCacheEntry(new ActionLayout() { SourceModelType = typeof(ActionModel) });
                    AddLayoutCacheEntry(new SelectionTaskLayout() { SourceModelType = typeof(DataObjectSelectionTaskModel) });

                    AddLayoutCacheEntry(new WorkspaceLayout() { SourceModelType = typeof(WorkspaceModel) });

                    AddLayoutCacheEntry(new TextValueSelectionLayout() { SourceModelType = typeof(ChooseReferencePropertyModel<String>), IsMultiline = false, AllowNullInput = false });
                    AddLayoutCacheEntry(new TextValueLayout() { SourceModelType = typeof(ReferencePropertyModel<String>), IsMultiline = false, AllowNullInput = false });
                    AddLayoutCacheEntry(new TextValueLayout() { SourceModelType = typeof(ObjectResultModel<String>), IsMultiline = false, AllowNullInput = false });

                    AddLayoutCacheEntry(new SimpleEnumValueLayout() { SourceModelType = typeof(EnumerationPropertyModel<int>).GetGenericTypeDefinition(), AllowNullInput = true });
                    AddLayoutCacheEntry(new ListValueLayout() { SourceModelType = typeof(SimpleReferenceListPropertyModel<string>).GetGenericTypeDefinition() });

                    AddLayoutCacheEntry(new DebuggerLayout() { SourceModelType = typeof(KistlDebuggerAsModel) });

                }
                return _defaultLayoutsCache;
            }
        }

        private static void AddLayoutCacheEntry(Layout layout)
        {
            if (_defaultLayoutsCache.ContainsKey(layout.SourceModelType))
                throw new InvalidOperationException(String.Format("Cannot overwrite existing DefaultLayout for {0}", layout.SourceModelType));

            _defaultLayoutsCache[layout.SourceModelType] = layout;
        }

        public static Layout LookupDefaultLayout(Type requested)
        {
            Layout result = null;
            var loop = requested;
            while (loop != null && !DefaultLayouts.TryGetValue(loop, out result))
            {
                loop = loop.BaseType;
            }

            if (requested.IsGenericType && !requested.IsGenericTypeDefinition)
            {
                // try without generic arguments too, e.g. for EnumerationPropertyModel`1
                Layout genericLayout = LookupDefaultLayout(requested.GetGenericTypeDefinition());

                // but only return the generic Layout if it is actually more 
                // specific than the one from the unmodified type
                if (result == null
                    || (result != null
                        && genericLayout != null
                        && (GenerationCount(typeof(Layout), genericLayout.GetType())
                            > GenerationCount(typeof(Layout), result.GetType()))))
                {
                    result = genericLayout;
                }
            }

            return result;
        }

        public static ViewDescriptor LookupViewDescriptor(Toolkit tk, Layout l)
        {
            Type layoutType = l.GetType();

            //var debug = Views
            //    .Where(vd => vd.Toolkit == tk && vd.LayoutRef.AsType(true).IsAssignableFrom(layoutType))
            //    .Select(vd => new { View = vd, Depth = GenerationCount(vd.LayoutRef.AsType(true), layoutType) })
            //    .OrderBy(p => p.Depth)
            //    .ToList();

            var result = Views
                // select matching descriptors
                .Where(vd => vd.Toolkit == tk && vd.LayoutRef.AsType(true).IsAssignableFrom(layoutType))
                // sort ViewRefs "near" the layout to the front
                .OrderBy(vd => GenerationCount(vd.LayoutRef.AsType(true), layoutType))
                // use the best match
                .First();

            return result;
        }

        private static int GenerationCount(Type parent, Type child)
        {
            Debug.Assert(parent.IsAssignableFrom(child));
            int result = 0;
            while (parent != child)
            {
                result += 1;
                child = child.BaseType;
            }
            return result;
        }

        private static TypeRef FindOrCreateTypeRef(IKistlContext ctx, string fullname, string assembly)
        {
            // Adapted from ToRef(Type,IKistlContext)
            var result = ctx.GetQuery<TypeRef>().SingleOrDefault(tRef => tRef.Assembly.AssemblyName == assembly && tRef.FullName == fullname && tRef.GenericArguments.Count == 0);
            if (result == null)
            {
                result = ctx.Create<TypeRef>();
                result.FullName = fullname;
                result.Assembly = ctx.GetQuery<Assembly>().SingleOrDefault(a => a.AssemblyName == assembly);
                if (result.Assembly == null)
                {
                    result.Assembly = ctx.Create<Assembly>();
                    result.Assembly.AssemblyName = assembly;
                    result.Assembly.Module = ctx.Find<Module>(4); // GUI Module
                }
            }
            return result;
        }

        private static TypeRef FindOrCreateTypeRef(IKistlContext ctx, Type t)
        {
            return t.ToRef(ctx);
        }

        private static List<ViewDescriptor> _viewsCache;
        public static List<ViewDescriptor> Views
        {
            get
            {
                if (_viewsCache == null)
                {
                    //using (var ctx = KistlContext.GetContext())
                    var ctx = FrozenContext.Single;
                    {
                        _viewsCache = new List<ViewDescriptor>()
                        {
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.WorkspaceView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(WorkspaceLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.Forms.View.WorkspaceView", "Kistl.Client.Forms"),
                                Toolkit.TEST, FindOrCreateTypeRef(ctx, typeof(WorkspaceLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.ASPNET.Toolkit.View.WorkspaceViewLoader", "Kistl.Client.ASPNET.Toolkit"),
                                Toolkit.ASPNET, FindOrCreateTypeRef(ctx, typeof(WorkspaceLayout))),

                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.DataObjectFullView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(DataObjectFullLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.Forms.View.DataObjectFullView", "Kistl.Client.Forms"),
                                Toolkit.TEST, FindOrCreateTypeRef(ctx, typeof(DataObjectFullLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.ASPNET.Toolkit.View.DataObjectFullViewLoader", "Kistl.Client.ASPNET.Toolkit"),
                                Toolkit.ASPNET, FindOrCreateTypeRef(ctx, typeof(DataObjectFullLayout))),

                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.ObjectReferenceView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(DataObjectReferenceLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.Forms.View.DataObjectReferenceView", "Kistl.Client.Forms"),
                                Toolkit.TEST, FindOrCreateTypeRef(ctx, typeof(DataObjectReferenceLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.ASPNET.Toolkit.View.DataObjectReferenceViewLoader", "Kistl.Client.ASPNET.Toolkit"),
                                Toolkit.ASPNET, FindOrCreateTypeRef(ctx, typeof(DataObjectReferenceLayout))),

                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.DataObjectListView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(DataObjectListLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.Forms.View.DataObjectListView", "Kistl.Client.Forms"),
                                Toolkit.TEST, FindOrCreateTypeRef(ctx, typeof(DataObjectListLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.ASPNET.Toolkit.View.DataObjectListViewLoader", "Kistl.Client.ASPNET.Toolkit"),
                                Toolkit.ASPNET, FindOrCreateTypeRef(ctx, typeof(DataObjectListLayout))),

                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.DataObjectView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(DataObjectLineLayout))),

                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.NullablePropertyTextBoxView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(Layout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.Forms.View.NullablePropertyTextBoxView", "Kistl.Client.Forms"),
                                Toolkit.TEST, FindOrCreateTypeRef(ctx, typeof(Layout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.ASPNET.Toolkit.View.NullablePropertyTextBoxViewLoader", "Kistl.Client.ASPNET.Toolkit"),
                                Toolkit.ASPNET, FindOrCreateTypeRef(ctx, typeof(Layout))),

                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.NullableBoolValueView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(SimpleNullableValueLayout<Boolean>))),

                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.SelectionDialog", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(SelectionTaskLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.ActionView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(ActionLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.TextValueSelectionView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(TextValueSelectionLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.EnumSelectionView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(SimpleEnumValueLayout))),
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.ListValueView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(ListValueLayout))), 
                            new ViewDescriptor(
                                FindOrCreateTypeRef(ctx, "Kistl.Client.WPF.View.KistlDebuggerView", "Kistl.Client.WPF"),
                                Toolkit.WPF, FindOrCreateTypeRef(ctx, typeof(DebuggerLayout))),
                        };
                        //ctx.SubmitChanges();
                    }
                }
                return _viewsCache;
            }
        }

        public static Kistl.App.Base.TypeRef LookupDefaultModelDescriptor(IDataObject obj)
        {
            return obj.GetObjectClass(FrozenContext.Single).GetDefaultModelRef();
        }

        public static Kistl.App.Base.TypeRef LookupDefaultPropertyModelDescriptor(Property p)
        {
            var ctx = FrozenContext.Single;

            // TODO: check model-override from instance/ObjectClass
            var prop = p as Property;

            if (p is BoolProperty && !prop.IsList)
            {
                return (typeof(NullableValuePropertyModel<Boolean>).ToRef(ctx));
            }
            else if (p is DateTimeProperty && !prop.IsList)
            {
                return (typeof(NullableValuePropertyModel<DateTime>).ToRef(ctx));
            }
            else if (p is DoubleProperty && !prop.IsList)
            {
                return (typeof(NullableValuePropertyModel<Double>).ToRef(ctx));
            }
            else if (p is IntProperty && !prop.IsList)
            {
                return (typeof(NullableValuePropertyModel<int>).ToRef(ctx));
            }
            else if (p is StringProperty)
            {
                if (p.ID == 77) // MethodInvocation.MemberName
                    return (typeof(ChooseReferencePropertyModel<string>).ToRef(ctx));
                else if (prop.IsList)
                    return (typeof(SimpleReferenceListPropertyModel<string>).ToRef(ctx));
                else
                    return (typeof(ReferencePropertyModel<string>).ToRef(ctx));
            }
            else if (p is ObjectReferenceProperty)
            {
                if (prop.IsList)
                    return (typeof(ObjectListModel).ToRef(ctx));
                else
                    return (typeof(ObjectReferenceModel).ToRef(ctx));
            }
            else if (p is EnumerationProperty)
            {
                var enumProp = p as EnumerationProperty;
                var enumRef = enumProp.Enumeration.GetDataType().ToRef(ctx);
                var genericRef = typeof(EnumerationPropertyModel<int>).GetGenericTypeDefinition();
                var genericAssembly = ctx.GetQuery<Assembly>().Single(a => a.AssemblyName == genericRef.Assembly.FullName);
                var concreteRef = ctx.GetQuery<Kistl.App.Base.TypeRef>()
                    .Where(r => r.FullName == genericRef.FullName
                        && r.Assembly.ID == genericAssembly.ID
                        && r.GenericArguments.Count == 1)
                    .ToList()
                    .SingleOrDefault(r => r.GenericArguments[0] == enumRef);
                if (concreteRef == null)
                {
                    concreteRef = ctx.Create<TypeRef>();
                    concreteRef.FullName = genericRef.FullName;
                    concreteRef.Assembly = genericAssembly;
                    concreteRef.GenericArguments.Add(enumRef);
                }
                return concreteRef;
            }
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for property: '{0}' of Type '{1}'", p, p.GetType()));
            }
        }

    }

}
