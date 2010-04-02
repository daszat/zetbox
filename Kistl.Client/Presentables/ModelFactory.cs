
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.GUI;

    /// <summary>
    /// Abstract base class to provide basic functionality of all model factories. Toolkit-specific implementations of this class will be 
    /// used by the rendering infrastructure to create ViewModels and Views.
    /// </summary>
    public abstract class ModelFactory : Kistl.Client.Presentables.IModelFactory
    {
        /// <summary>
        /// Gets this application's global context. A helper.
        /// </summary>
        protected IGuiApplicationContext AppContext { get; private set; }

        /// <summary>
        /// Gets the Toolkit of the implementation. A constant.
        /// </summary>
        protected abstract Toolkit Toolkit { get; }

        protected ModelFactory(IGuiApplicationContext appCtx)
        {
            AppContext = appCtx;
            AppContext.UiThread.Verify();
            this.Managers = new Dictionary<IKistlContext, IMultipleInstancesManager>();
        }

        #region Model Management

        private ModelCache _cache = new ModelCache();

        /// <summary>
        /// Should only be used in very "special" situations. Using
        /// <see cref="CreateDefaultModel"/> is usually much better.
        /// </summary>
        /// <param name="ctx">the data context to use</param>
        /// <param name="data">the arguments to pass to the model's constructor</param>
        public TModel CreateSpecificModel<TModel>(IKistlContext ctx, params object[] data)
            where TModel : ViewModel
        {
            Type requestedType = typeof(TModel);
            return (TModel)CreateModel(requestedType, ctx, data);
        }

        /// <summary>
        /// Creates a default model for the object <paramref name="obj"/>
        /// </summary>
        /// <param name="ctx">the data context to use</param>
        /// <param name="obj">the object to model</param>
        /// <param name="data">the arguments to pass to the model's constructor</param>
        /// <returns>the configured model</returns>
        public ViewModel CreateDefaultModel(IKistlContext ctx, IDataObject obj, params object[] data)
        {
            Type t = obj.GetObjectClass(AppContext.MetaContext)
                .DefaultViewModelDescriptor
                .ViewModelRef
                .AsType(true);
            return CreateModel(t, ctx, new object[] { obj }.Concat(data).ToArray());
        }

        /// <summary>
        /// Creates a ViewModel to display/edit the value of the property p of the object obj.
        /// </summary>
        /// <param name="ctx">the data context to use</param>
        /// <param name="obj">the referenced object</param>
        /// <param name="p">the property whose value shall be displayed</param>
        /// <returns>a properly initialised ViewModel</returns>
        public ViewModel CreatePropertyValueModel(IKistlContext ctx, IDataObject obj, Property p)
        {
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (obj == null) { throw new ArgumentNullException("obj"); }
            if (p == null) { throw new ArgumentNullException("p"); }

            if (p.ValueModelDescriptor != null)
            {
                return CreateModel(p.ValueModelDescriptor
                    .ViewModelRef
                    .AsType(true), ctx, new object[] { obj, p });
            }
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for property: '{0}' of Type '{1}'", p, p.GetType()));
            }
        }

        public ViewModel CreateModel(Type requestedType, IKistlContext ctx, object[] data)
        {

            // by convention, all presentable models take the IGuiApplicationContext
            // and a IKistlContext as first parameters
            object[] parameters = new object[] { AppContext, ctx }.Concat(data).ToArray();

            ViewModel result = _cache.LookupModel(requestedType, parameters);

            if (result == null)
            {
                try
                {
                    result = (ViewModel)Activator.CreateInstance(requestedType, parameters);
                }
                catch (Exception ex)
                {
                    Logging.Log.Error(string.Format("Error creating model for requestedType [{0}]", requestedType.FullName), ex);
                    return null;
                }
                _cache.StoreModel(parameters, result);
            }

            // save workspaces
            if (typeof(IMultipleInstancesManager).IsAssignableFrom(requestedType))
            {
                OnIMultipleInstancesManagerCreated(ctx, (IMultipleInstancesManager)result);
            }

            return result;
        }

        #endregion

        #region Top-Level Views Management

        /// <summary>
        /// Creates a default View for the given ViewModel.
        /// </summary>
        /// <param name="mdl">the model to be viewed</param>
        /// <returns>the configured view</returns>
        public virtual object CreateDefaultView(ViewModel mdl)
        {
            if (mdl == null) { throw new ArgumentNullException("mdl"); }

            ViewModelDescriptor pmd = mdl
                .GetType()
                .ToRef(GuiApplicationContext.Current.MetaContext)
                .GetViewModelDescriptor();

            var vDesc = pmd.GetViewDescriptor(Toolkit);

            return vDesc == null
                ? null
                : CreateView(vDesc);
        }

        /// <summary>
        /// Creates a view from a view descriptor. By default it just creates a new instance of the ControlRef
        /// </summary>
        /// <param name="vDesc">the descriptor describing the view</param>
        /// <returns>a newly created view</returns>
        protected virtual object CreateView(ViewDescriptor vDesc)
        {
            return vDesc.ControlRef.Create();
        }

        /// <summary>
        /// Creates a specific View for the given ViewModel.
        /// </summary>
        /// <param name="mdl">the model to be viewed</param>
        /// <param name="kind">the kind of view to create</param>
        /// <returns>the configured view</returns>
        public virtual object CreateSpecificView(ViewModel mdl, ControlKind kind)
        {
            if (mdl == null) { throw new ArgumentNullException("mdl"); }
            if (kind == null) { throw new ArgumentNullException("kind"); }

            ViewModelDescriptor pmd = mdl.GetType().ToRef(GuiApplicationContext.Current.MetaContext)
                .GetViewModelDescriptor();

            var vDesc = pmd.GetViewDescriptor(Toolkit, kind);

            return vDesc == null
                ? null
                : CreateView(vDesc);
        }

        public void ShowModel(ViewModel mdl, ControlKind kind, bool activate)
        {
            ShowInView(mdl, CreateSpecificView(mdl, kind), activate);
        }

        /// <summary>
        /// Shows the specified model. 
        /// </summary>
        /// <param name="mdl"></param>
        /// <param name="activate"></param>
        public void ShowModel(ViewModel mdl, bool activate)
        {
            if (mdl == null)
                throw new ArgumentNullException("mdl");

            var dom = mdl as DataObjectModel;

            if (dom == null)
            {
                ShowInView(mdl, CreateDefaultView(mdl), activate);
            }
            else
            {
                var m = Managers[dom.Object.Context];
                m.HistoryTouch(dom);
                m.SelectedItem = dom;
            }
        }

        protected abstract void ShowInView(ViewModel mdl, object view, bool activate);

        #endregion

        #region Workspace Management

        protected Dictionary<IKistlContext, IMultipleInstancesManager> Managers { get; private set; }

        protected virtual void OnIMultipleInstancesManagerCreated(IKistlContext ctx, IMultipleInstancesManager workspace)
        {
            // TODO: limit this reference to the lifetime of the context
            this.Managers[ctx] = workspace;
        }

        #endregion

        #region other gui infrastructure

        public abstract void CreateTimer(TimeSpan tickLength, Action action);

        /// <summary>
        /// Asks the user for a filename to load from.
        /// </summary>
        /// <param name="filter">a list of filters for files to display in the format of "Label|glob1[;glob2[;...]"</param>
        /// <returns>the chosen file name or <code>String.Empty</code> if the user aborted the selection</returns>
        public abstract string GetSourceFileNameFromUser(params string[] filter);

        #endregion

        internal sealed class ModelCache
        {
            /// <summary>
            /// a map of all models created from this factory.
            /// </summary>
            /// uses Type as outer parameter to keep number of second level dictionaries small
            // TODO: memory: investigate using a weakly referencing proxy to object[] as 2nd level key,
            //               but probably all data params are rooted elsewhere too. Should clean up
            //               at least when the IKistlContext of a Workspace is disposed
            private Dictionary<Type, Dictionary<object[], ViewModel>> _models
                    = new Dictionary<Type, Dictionary<object[], ViewModel>>();

            internal ViewModel LookupModel(Type requestedType, object[] parameters)
            {
                Dictionary<object[], ViewModel> modelCache;
                if (!_models.TryGetValue(requestedType, out modelCache))
                {
                    // top level entry doesn't exist
                    return null;
                }

                ViewModel result = null;
                if (!modelCache.TryGetValue(parameters, out result))
                {
                    return null;
                }
                return result;
            }

            internal void StoreModel(object[] parameters, ViewModel mdl)
            {
                Type requestedType = mdl.GetType();

                Dictionary<object[], ViewModel> modelCache;
                if (!_models.TryGetValue(requestedType, out modelCache))
                {
                    // create new top-level entry
                    modelCache = new Dictionary<object[], ViewModel>(new ObjectArrayComparer());
                    _models[requestedType] = modelCache;
                }

                modelCache[parameters] = mdl;
            }
        }

        /// <summary>
        /// an <see cref="IEqualityComparer&lt;T>"/> which compares object arrays for memberwise 
        /// equality and calculates an appropriate hashcode
        /// </summary>
        internal sealed class ObjectArrayComparer : IEqualityComparer<object[]>
        {
            #region IEqualityComparer<object[]> Members

            /// <inheritdoc/>
            public bool Equals(object[] x, object[] y)
            {
                bool result = true;

                if (x.Length != y.Length)
                {
                    return false;
                }

                for (int i = 0;
                    // abort on first miss
                    result && i < x.Length;
                    i++)
                {
                    if (x[i] != null && y[i] != null)
                    {
                        result &= x[i].Equals(y[i]);
                    }
                    else
                    {
                        result &= x[i] == y[i]; // only true if both x[i] and y[i] are null
                    }
                }
                return result;
            }

            /// <inheritdoc/>
            public int GetHashCode(object[] objs)
            {
                // calculate the XOR of all not null elements of objs
                return objs.Where(o => o != null).Aggregate(0, (acc, o) => (acc ^= o.GetHashCode()));
            }

            #endregion
        }
    }
}
