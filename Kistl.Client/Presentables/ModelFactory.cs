using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;
using Kistl.App.GUI;
using Kistl.Client.GUI.DB;
using Kistl.Client.GUI;
using System.ComponentModel;

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
            Type t = obj.GetObjectClass(AppContext.FrozenContext).GetDefaultModelRef().AsType(true); ;
            return CreateModel(t, ctx, new object[] { obj }.Concat(data).ToArray());
        }

        /// <summary>
        /// deprecated helper
        /// </summary>
        [Browsable(false)]
        public PresentableModel CreateModel(Kistl.App.Base.TypeRef modelType, IKistlContext ctx, object[] data)
        {
            return CreateModel(modelType.AsType(true), ctx, data);
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

        public IView CreateDefaultView(PresentableModel mdl)
        {
            Layout lout = DataMocks.LookupDefaultLayout(mdl.GetType());
            ViewDescriptor vDesc = DataMocks.LookupViewDescriptor(Toolkit, lout);
            IView view = vDesc.ViewRef.Create();
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

        //public void ShowModel(PresentableModel mdl, TContainer parent)
        //{
        //    Layout lout = DataMocks.LookupDefaultLayout(mdl.GetType());
        //    ViewDescriptor vDesc = DataMocks.LookupViewDescriptor(Toolkit, lout);
        //    ShowInViewInContainer(vDesc.ViewRef.Create(), mdl, parent);
        //}
        //protected abstract void ShowInViewInContainer(PresentableModel mdl, TView view, TContainer parent);

        #endregion
    }

    internal sealed class ModelCache
    {
        /// <summary>
        /// a map of all models created from this factory.
        /// </summary>
        /// uses Type as outer parameter to keep number of second level dictionaries small
        // TODO: memory: investigate using a weakly referencing proxy to object[] as 2nd level key,
        //               but probably all data params are rooted elsewhere too.
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
