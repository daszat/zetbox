using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.Base;
using Kistl.App.Extensions;
using Kistl.App.GUI;
using Kistl.Client.GUI;

namespace Kistl.Client.Presentables
{
    public abstract class ModelFactory
    {
        /// <summary>
        /// This application's global context
        /// </summary>
        protected IGuiApplicationContext AppContext { get; private set; }

        protected abstract Toolkit Toolkit { get; }
        protected abstract object Renderer { get; }

        protected WorkspaceModel Workspace { get; private set; }
        protected virtual void OnWorkspaceCreated() { }

        protected ModelFactory(IGuiApplicationContext appCtx)
        {
            AppContext = appCtx;
            AppContext.UiThread.Verify();
        }

        #region Model Management

        private ModelCache _cache = new ModelCache();

        /// <summary>
        /// Should only be used in very "special" situations. Using
        /// <see cref="CreateDefaultModel"/> is usually much better.
        /// </summary>
        public TModel CreateSpecificModel<TModel>(IKistlContext ctx, params object[] data)
            where TModel : PresentableModel
        {
            Type requestedType = typeof(TModel);
            return (TModel)CreateModel(requestedType, ctx, data);
        }

        /// <summary>
        /// Creates a default model for the object <value>obj</value>.
        /// </summary>
        /// <returns>the configured model</returns>
        public PresentableModel CreateDefaultModel(IKistlContext ctx, IDataObject obj, params object[] data)
        {
            Type t = obj.GetObjectClass(AppContext.MetaContext)
                .DefaultPresentableModelDescriptor
                .PresentableModelRef
                .AsType(true); ;
            return CreateModel(t, ctx, new object[] { obj }.Concat(data).ToArray());
        }

        /// <summary>
        /// Creates a PresentableModel to display/edit the value of the property p of the object obj.
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public PresentableModel CreatePropertyValueModel(IKistlContext ctx, IDataObject obj, Property p)
        {
            // TODO: check model-override from instance/ObjectClass
            // TODO: implement common ObjectClasses[Property].{DefaultValueModel,DefaultListValueModel}
            //       and specific Property.{DefaultValueModel,DefaultListValueModel}

            //if (p.IsList)
            //{
            //    return CreateModel((p.DefaultListValueModel ?? obj.GetObjectClass(AppContext.MetaContext).DefaultListValueModel), ctx, new object[] { obj, p });
            //}
            //else
            //{
            //    return CreateModel((p.DefaultValueModel ?? obj.GetObjectClass(AppContext.MetaContext).DefaultValueModel), ctx, new object[] { obj, p });
            //}

            if (p is BoolProperty && !p.IsList)
            {
                return CreateSpecificModel<NullableValuePropertyModel<bool>>(ctx, obj, p);
            }
            else if (p is DateTimeProperty && !p.IsList)
            {
                return CreateSpecificModel<NullableValuePropertyModel<DateTime>>(ctx, obj, p);
            }
            else if (p is DoubleProperty && !p.IsList)
            {
                return CreateSpecificModel<NullableValuePropertyModel<double>>(ctx, obj, p);
            }
            else if (p is IntProperty && !p.IsList)
            {
                return CreateSpecificModel<NullableValuePropertyModel<int>>(ctx, obj, p);
            }
            else if (p is StringProperty)
            {
                if (p.ID == 77) // MethodInvocation.MemberName
                    return CreateSpecificModel<ChooseReferencePropertyModel<string>>(ctx, obj, p);
                else if (p.IsList)
                    return CreateSpecificModel<SimpleReferenceListPropertyModel<string>>(ctx, obj, p);
                else
                    return CreateSpecificModel<ReferencePropertyModel<string>>(ctx, obj, p);
            }
            else if (p is ObjectReferenceProperty)
            {
                if (p.IsList)
                    return CreateSpecificModel<ObjectListModel>(ctx, obj, p);
                else
                    return CreateSpecificModel<ObjectReferenceModel>(ctx, obj, p);
            }
            //else if (p is EnumerationProperty)
            //{
            //    switch (p.ID)
            //    {
            //        case 2: return CreateSpecificModel<EnumerationPropertyModel<Toolkit>>(ctx, obj, p);
            //        case 5: return CreateSpecificModel<EnumerationPropertyModel<VisualType>>(ctx, obj, p);
            //            ...
            //    }
            //    var enumProp = p as EnumerationProperty;
            //    var enumRef = enumProp.Enumeration.GetDataType().ToRef(ctx);
            //    var genericRef = typeof(EnumerationPropertyModel<int>).GetGenericTypeDefinition();
            //    var genericAssembly = ctx.GetQuery<Assembly>().Single(a => a.AssemblyName == genericRef.Assembly.FullName);
            //    var concreteRef = ctx.GetQuery<Kistl.App.Base.TypeRef>()
            //        .Where(r => r.FullName == genericRef.FullName
            //            && r.Assembly.ID == genericAssembly.ID
            //            && r.GenericArguments.Count == 1)
            //        .ToList()
            //        .SingleOrDefault(r => r.GenericArguments[0] == enumRef);
            //    if (concreteRef == null)
            //    {
            //        concreteRef = ctx.Create<TypeRef>();
            //        concreteRef.FullName = genericRef.FullName;
            //        concreteRef.Assembly = genericAssembly;
            //        concreteRef.GenericArguments.Add(enumRef);
            //    }
            //    return concreteRef;
            //}
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for property: '{0}' of Type '{1}'", p, p.GetType()));
            }
        }


        public PresentableModel CreateModel(Type requestedType, IKistlContext ctx, object[] data)
        {

            // by convention, all presentable models take the IGuiApplicationContext
            // and a IKistlContext as first parameters
            object[] parameters = new object[] { AppContext, ctx }.Concat(data).ToArray();

            PresentableModel result = _cache.LookupModel(requestedType, parameters);

            if (result == null)
            {
                result = (PresentableModel)Activator.CreateInstance(requestedType, parameters);
                _cache.StoreModel(parameters, result);
            }

            // save first workspace
            if (typeof(WorkspaceModel).IsAssignableFrom(requestedType) && this.Workspace == null)
            {
                this.Workspace = (WorkspaceModel)result;
                OnWorkspaceCreated();
            }

            return result;
        }

        #endregion

        #region Top-Level Views Management

        /// <summary>
        /// Creates a default View for the given PresentableModel.
        /// </summary>
        /// <param name="mdl"></param>
        /// <returns></returns>
        public IView CreateDefaultView(PresentableModel mdl)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single)
                .GetPresentableModelDescriptor();

            var vDesc = pmd.GetDefaultViewDescriptor(Toolkit);
            IView view = (IView)vDesc.ControlRef.Create();
            view.SetModel(mdl);
            return view;
        }

        public void ShowModel(PresentableModel mdl, bool activate)
        {
            if (mdl is DataObjectModel)
            {
                // TODO improve multi-workspace facitilities
                Workspace.SelectedItem = (DataObjectModel)mdl;
            }
            else
            {
                ShowInView(Renderer, mdl, CreateDefaultView(mdl), activate);
            }
        }

        protected abstract void ShowInView(object renderer, PresentableModel mdl, IView view, bool activate);

        #endregion
    }

    internal sealed class ModelCache
    {
        /// <summary>
        /// a map of all models created from this factory.
        /// </summary>
        /// uses Type as outer parameter to keep number of second level dictionaries small
        // TODO: memory: investigate using a weakly referencing proxy to object[] as 2nd level key,
        //               but probably all data params are rooted elsewhere too. Should clean up
        //               at least when the IKistlContext of a Workspace is disposed
        private Dictionary<Type, Dictionary<object[], PresentableModel>> _models
                = new Dictionary<Type, Dictionary<object[], PresentableModel>>();

        internal PresentableModel LookupModel(Type requestedType, object[] parameters)
        {
            Dictionary<object[], PresentableModel> modelCache;
            if (!_models.TryGetValue(requestedType, out modelCache))
            {
                // top level entry doesn't exist
                return null;
            }

            PresentableModel result = null;
            if (!modelCache.TryGetValue(parameters, out result))
            {
                return null;
            }
            return result;
        }

        internal void StoreModel(object[] parameters, PresentableModel mdl)
        {
            Type requestedType = mdl.GetType();

            Dictionary<object[], PresentableModel> modelCache;
            if (!_models.TryGetValue(requestedType, out modelCache))
            {
                // create new top-level entry
                modelCache = new Dictionary<object[], PresentableModel>(new ObjectArrayComparer());
                _models[requestedType] = modelCache;
            }

            modelCache[parameters] = mdl;
        }
    }

    /// <summary>
    /// an <see cref="IEqualityComparer<>"/> which compares object arrays for memberwise 
    /// equality and calculates an appropriate hashcode
    /// </summary>
    internal sealed class ObjectArrayComparer : IEqualityComparer<object[]>
    {
        #region IEqualityComparer<object[]> Members

        public bool Equals(object[] x, object[] y)
        {
            bool result = true;
            if (x.Length != y.Length)
                return false;

            for (int i = 0;
                // abort on first miss
                result && i < x.Length;
                i++)
            {
                if (x[i] != null && y[i] != null)
                    result &= x[i].Equals(y[i]);
                else
                    result &= (x[i] == y[i]); // only true if both x[i] and y[i] are null
            }
            return result;
        }

        public int GetHashCode(object[] objs)
        {
            // calculate the XOR of all not null elements of objs
            return objs.Where(o => o != null).Aggregate(0, (acc, o) => (acc ^= o.GetHashCode()));
        }

        #endregion
    }

}
