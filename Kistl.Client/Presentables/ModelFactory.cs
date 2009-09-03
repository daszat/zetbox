
namespace Kistl.Client.Presentables
{
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

    /// <summary>
    /// Abstract base class to provide basic functionality of all model factories. Toolkit-specific implementations of this class will be 
    /// used by the rendering infrastructure to create PresentableModels and Views.
    /// </summary>
    public abstract class ModelFactory
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
            this.Workspaces = new Dictionary<IKistlContext, WorkspaceModel>();
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
            where TModel : PresentableModel
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
        public PresentableModel CreateDefaultModel(IKistlContext ctx, IDataObject obj, params object[] data)
        {
            Type t = obj.GetObjectClass(AppContext.MetaContext)
                .DefaultPresentableModelDescriptor
                .PresentableModelRef
                .AsType(true);
            return CreateModel(t, ctx, new object[] { obj }.Concat(data).ToArray());
        }

        /// <summary>
        /// Creates a PresentableModel to display/edit the value of the property p of the object obj.
        /// </summary>
        /// <param name="ctx">the data context to use</param>
        /// <param name="obj">the referenced object</param>
        /// <param name="p">the property whose value shall be displayed</param>
        /// <returns>a properly initialised PresentableModel</returns>
        public PresentableModel CreatePropertyValueModel(IKistlContext ctx, IDataObject obj, Property p)
        {
            if (p.ValueModelDescriptor != null)
            {
                return CreateModel(p.ValueModelDescriptor
                    .PresentableModelRef
                    .AsType(true), ctx, new object[] { obj, p });
            }
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

            // save workspaces
            if (typeof(WorkspaceModel).IsAssignableFrom(requestedType))
            {
                OnWorkspaceCreated(ctx, (WorkspaceModel)result);
            }

            return result;
        }

        #endregion

        #region Top-Level Views Management

        /// <summary>
        /// Creates a default View for the given PresentableModel.
        /// </summary>
        /// <param name="mdl">the model to be viewed</param>
        /// <param name="readOnly">a value indicating whether or not the view should be read only</param>
        /// <returns>the configured view</returns>
        public IView CreateDefaultView(PresentableModel mdl)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single)
                .GetPresentableModelDescriptor();

            var vDesc = pmd.GetDefaultViewDescriptor(Toolkit);
            IView view = CreateView(vDesc);
            view.SetModel(mdl);
            return view;
        }

        /// <summary>
        /// Creates a view from a view descriptor. By default it just creates a new instance of the ControlRef
        /// </summary>
        /// <param name="vDesc">the descriptor describing the view</param>
        /// <returns>a newly created view</returns>
        protected virtual IView CreateView(ViewDescriptor vDesc)
        {
            return (IView)vDesc.ControlRef.Create();
        }

        /// <summary>
        /// Creates a specific View for the given PresentableModel.
        /// </summary>
        /// <param name="mdl">the model to be viewed</param>
        /// <param name="kind">the kind of view to create</param>
        /// <param name="readOnly">a value indicating whether or not the view should be read only</param>
        /// <returns>the configured view</returns>
        public IView CreateSpecificView(PresentableModel mdl, ControlKind kind)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single)
                .GetPresentableModelDescriptor();

            var vDesc = pmd.GetViewDescriptor(Toolkit, kind);
            IView view = CreateView(vDesc);
            view.SetModel(mdl);
            return view;
        }

        public void ShowModel(PresentableModel mdl, ControlKind kind, bool activate)
        {
            ShowInView(mdl, CreateSpecificView(mdl, kind), activate);
        }

        /// <summary>
        /// Shows the specified model. 
        /// </summary>
        /// <param name="mdl"></param>
        /// <param name="activate"></param>
        public void ShowModel(PresentableModel mdl, bool activate)
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
                var ws = Workspaces[dom.Object.Context];
                ws.HistoryTouch(dom);
                ws.SelectedItem = dom;
            }
        }

        protected abstract void ShowInView(PresentableModel mdl, IView view, bool activate);

        #endregion

        #region Workspace Management

        protected Dictionary<IKistlContext, WorkspaceModel> Workspaces { get; private set; }

        protected virtual void OnWorkspaceCreated(IKistlContext ctx, WorkspaceModel workspace)
        {
            // TODO: limit this reference to the lifetime of the context
            this.Workspaces[ctx] = workspace;
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
