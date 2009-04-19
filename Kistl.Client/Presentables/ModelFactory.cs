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
