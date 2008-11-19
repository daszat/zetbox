using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.API;

namespace Kistl.Client.Presentables
{
    public abstract class ModelFactory
    {
        /// <summary>
        /// This application's global context
        /// </summary>
        protected IGuiApplicationContext AppContext { get; private set; }

        protected ModelFactory(IGuiApplicationContext appCtx)
        {
            AppContext = appCtx;
            AppContext.UiThread.Verify();
        }

        private ModelCache _cache = new ModelCache();

        public TModel CreateSpecificModel<TModel>(IKistlContext ctx, params object[] data)
            where TModel : PresentableModel
        {
            // TODO: use data store to select proper type
            Type requestedType = typeof(TModel);

            // by convention, all presentable models take the IGuiApplicationContext
            // and a IKistlContext as first parameters
            object[] parameters = new object[] { AppContext, ctx }.Concat(data).ToArray();

            PresentableModel result = _cache.LookupModel(requestedType, parameters);

            if (result == null)
            {
                result = (PresentableModel)Activator.CreateInstance(requestedType, parameters);
                _cache.StoreModel(parameters, result);
            }

            return (TModel)result;
        }

        public void ShowModel(PresentableModel mdl, bool activate)
        {
            if (mdl is WorkspaceModel)
            {
                CreateWorkspace((WorkspaceModel)mdl, activate);
            }
            else if (mdl is DataObjectSelectionTaskModel)
            {
                CreateSelectionDialog((DataObjectSelectionTaskModel)mdl, activate);
            }
            else if (mdl is DataObjectModel)
            {
                ShowDataObject((DataObjectModel)mdl, activate);
            }
            else
            {
                throw new InvalidOperationException(String.Format("Cannot show object '{0}' of Type '{1}'", mdl, mdl.GetType()));
            }
        }

        protected abstract void CreateSelectionDialog(DataObjectSelectionTaskModel selectionTaskModel, bool activate);
        protected abstract void CreateWorkspace(WorkspaceModel workspace, bool activate);
        protected abstract void ShowDataObject(DataObjectModel dataObject, bool activate);
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
