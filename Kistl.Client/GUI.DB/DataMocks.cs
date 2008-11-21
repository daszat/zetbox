using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.Client.Presentables;
using Kistl.App.GUI;
using Kistl.API;
using Kistl.App.Base;
using System.Diagnostics;

namespace Kistl.Client.GUI.DB
{
    public sealed class TypeRef
    {
        public TypeRef(string name, string assembly, params TypeRef[] genericArgs)
        {
            FullName = name;
            Assembly = assembly;
            GenericArguments = genericArgs;
        }
        public TypeRef(Type t, params TypeRef[] genericArgs)
        {
            FullName = t.FullName;
            Assembly = t.Assembly.FullName;
            GenericArguments = genericArgs;
        }

        public string FullName { get; private set; }
        public string Assembly { get; private set; }
        public TypeRef[] GenericArguments { get; private set; }

        public Type AsType()
        {
            var result = Type.GetType(String.Format("{0}, {1}", FullName, Assembly), true);
            if (GenericArguments.Length > 0)
            {
                result.MakeGenericType(GenericArguments.Select(tRef => tRef.AsType()).ToArray());
            }
            return result;
        }

        public object Create(params object[] parameter)
        {
            return Activator.CreateInstance(this.AsType(), parameter);
        }

        public override bool Equals(object obj)
        {
            TypeRef other = obj as TypeRef;
            if (this == other)
            {
                return true;
            }
            if (other != null)
            {
                return other.FullName == this.FullName
                    && other.Assembly == this.Assembly
                    && other.GenericArguments.SequenceEqual(this.GenericArguments);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return FullName.GetHashCode() + Assembly.GetHashCode() + GenericArguments.Sum(r => r.GetHashCode());
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
    }

    public class ModelDescriptor
    {

        public ModelDescriptor(TypeRef presentationType)
        {
            Presentation = presentationType;
        }

        public TypeRef Presentation { get; private set; }

    }

    public abstract class Layout
    {
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
    /// layout a value as text
    /// </summary>
    public class SimpleValueLayout<TValue> : Layout
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
    /// how to layout a text value
    /// </summary>
    public class TextValueLayout : SimpleNullableValueLayout<string>
    {
        public bool IsMultiline { get; set; }
    }

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

                    AddLayoutCacheEntry(new SelectionTaskLayout() { SourceModelType = typeof(DataObjectSelectionTaskModel) });

                    AddLayoutCacheEntry(new WorkspaceLayout() { SourceModelType = typeof(WorkspaceModel) });

                    AddLayoutCacheEntry(new TextValueLayout() { SourceModelType = typeof(ReferencePropertyModel<String>), IsMultiline = false, AllowNullInput = false });
                    AddLayoutCacheEntry(new TextValueLayout() { SourceModelType = typeof(ObjectResultModel<String>), IsMultiline = false, AllowNullInput = false });

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

        public static Layout LookupDefaultLayout(Type t)
        {
            Layout result = null;
            while (t != null && !DefaultLayouts.TryGetValue(t, out result))
            {
                t = t.BaseType;
            }
            return result;
        }
        public static ViewDescriptor LookupViewDescriptor(Toolkit tk, Layout l)
        {
            Type layoutType = l.GetType();
            var result = Views
                // select matching descriptors
                .Where(vd => vd.Toolkit == tk && vd.LayoutRef.AsType().IsAssignableFrom(layoutType))
                // sort ViewRefs "near" the layout to the front
                .OrderBy(vd => GenerationCount(vd.LayoutRef.AsType(), layoutType))
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

        private static List<ViewDescriptor> _viewsCache;
        public static List<ViewDescriptor> Views
        {
            get
            {
                if (_viewsCache == null)
                {
                    _viewsCache = new List<ViewDescriptor>()
                    {
                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.WPF.View.WorkspaceView", "Kistl.Client.WPF"),
                            Toolkit.WPF, new TypeRef(typeof(WorkspaceLayout))),
                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.Forms.View.WorkspaceView", "Kistl.Client.Forms"),
                            Toolkit.TEST, new TypeRef(typeof(WorkspaceLayout))),

                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.WPF.View.DataObjectFullView", "Kistl.Client.WPF"),
                            Toolkit.WPF, new TypeRef(typeof(DataObjectFullLayout))),
                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.Forms.View.DataObjectFullView", "Kistl.Client.Forms"),
                            Toolkit.TEST, new TypeRef(typeof(DataObjectFullLayout))),

                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.WPF.View.ObjectReferenceView", "Kistl.Client.WPF"),
                            Toolkit.WPF, new TypeRef(typeof(DataObjectReferenceLayout))),
                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.Forms.View.DataObjectReferenceView", "Kistl.Client.Forms"),
                            Toolkit.TEST, new TypeRef(typeof(DataObjectReferenceLayout))),

                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.WPF.View.DataObjectListView", "Kistl.Client.WPF"),
                            Toolkit.WPF, new TypeRef(typeof(DataObjectListLayout))),
                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.Forms.View.DataObjectListView", "Kistl.Client.Forms"),
                            Toolkit.TEST, new TypeRef(typeof(DataObjectListLayout))),

                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.WPF.View.DataObjectView", "Kistl.Client.WPF"),
                            Toolkit.WPF, new TypeRef(typeof(DataObjectLineLayout))),

                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.WPF.View.NullablePropertyTextBoxView", "Kistl.Client.WPF"),
                            Toolkit.WPF, new TypeRef(typeof(Layout))),
                        new ViewDescriptor(
                            new TypeRef("Kistl.Client.Forms.View.NullablePropertyTextBoxView", "Kistl.Client.Forms"),
                            Toolkit.TEST, new TypeRef(typeof(Layout))),
                    };
                }
                return _viewsCache;
            }
        }

        public static ModelDescriptor LookupDefaultModelDescriptor(IDataObject obj)
        {
            if (obj is BaseProperty)
            {
                return LookupDefaultPropertyModelDescriptor((BaseProperty)obj);
            }
            else if (obj is Module)
            {
                return new ModelDescriptor(new TypeRef(typeof(ModuleModel)));
            }

            throw new NotImplementedException(String.Format("==>> No model for object: '{0}' of Type '{1}'", obj, obj.GetType()));
        }

        public static ModelDescriptor LookupDefaultPropertyModelDescriptor(BaseProperty p)
        {
            // TODO: check model-override from instance/ObjectClass
            var prop = p as Property;

            if (p is BoolProperty && !prop.IsList)
            {
                return new ModelDescriptor(new TypeRef(typeof(NullableValuePropertyModel<Boolean>)));
            }
            else if (p is DateTimeProperty && !prop.IsList)
            {
                return new ModelDescriptor(new TypeRef(typeof(NullableValuePropertyModel<DateTime>)));
            }
            else if (p is DoubleProperty && !prop.IsList)
            {
                return new ModelDescriptor(new TypeRef(typeof(NullableValuePropertyModel<Double>)));
            }
            else if (p is IntProperty && !prop.IsList)
            {
                return new ModelDescriptor(new TypeRef(typeof(NullableValuePropertyModel<int>)));
            }
            else if (p is StringProperty && !prop.IsList)
            {
                return new ModelDescriptor(new TypeRef(typeof(ReferencePropertyModel<string>)));
            }
            else if (p is ObjectReferenceProperty)
            {
                if (prop.IsList)
                    return new ModelDescriptor(new TypeRef(typeof(ObjectListModel)));
                else
                    return new ModelDescriptor(new TypeRef(typeof(ObjectReferenceModel)));
            }
            else if (p is BackReferenceProperty)
            {
                return new ModelDescriptor(new TypeRef(typeof(ObjectListModel)));
            }
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for property: '{0}' of Type '{1}'", p, p.GetType()));
            }

        }

    }


}
