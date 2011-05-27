
namespace Kistl.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using Autofac;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Configuration;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables.ValueViewModels;

    /// <summary>
    /// Abstract base class to provide basic functionality of all model factories. Toolkit-specific implementations of this class will be 
    /// used by the rendering infrastructure to create ViewModels and Views.
    /// </summary>
    public abstract class ViewModelFactory : Kistl.Client.Presentables.IViewModelFactory
    {
        /// <summary>
        /// Gets the Toolkit of the implementation. A constant.
        /// </summary>
        public abstract Toolkit Toolkit { get; }

        protected readonly Autofac.ILifetimeScope Container;
        protected readonly IFrozenContext FrozenContext;
        protected readonly KistlConfig Configuration;

        private struct VMCacheKey
        {
            public VMCacheKey(Type requestedType, Type factoryType)
            {
                this.requestedType = requestedType;
                this.factoryType = factoryType;
            }

            public Type requestedType;
            public Type factoryType;

            public override bool Equals(object obj)
            {
                if (obj is VMCacheKey)
                {
                    var other = (VMCacheKey)obj;
                    return this.requestedType == other.requestedType && this.factoryType == other.factoryType;
                }
                else
                {
                    return false;
                }
            }

            public override int GetHashCode()
            {
                return requestedType.GetHashCode() + factoryType.GetHashCode();
            }
        }

        private readonly Dictionary<VMCacheKey, object> _viewModelFactoryCache;

        protected ViewModelFactory(Autofac.ILifetimeScope container, IFrozenContext frozenCtx, KistlConfig cfg)
        {
            if (container == null) throw new ArgumentNullException("container");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (cfg == null) throw new ArgumentNullException("cfg");

            this.Container = container;
            this.FrozenContext = frozenCtx;
            this.Configuration = cfg;
            this.Managers = new Dictionary<IKistlContext, IMultipleInstancesManager>();
            this._viewModelFactoryCache = new Dictionary<VMCacheKey, object>();
        }

        #region Model Management
        public TModelFactory CreateViewModel<TModelFactory>() where TModelFactory : class
        {
            return CreateViewModel<TModelFactory>(typeof(TModelFactory));
        }

        public TModelFactory CreateViewModel<TModelFactory>(IDataObject obj) where TModelFactory : class
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var desc = obj.GetObjectClass(FrozenContext)
                .DefaultViewModelDescriptor;
            if (desc != null)
            {
                return CreateViewModel<TModelFactory>(desc);
            }
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for object class: '{0}'", obj.GetType()));
            }
        }

        public TModelFactory CreateViewModel<TModelFactory>(ICompoundObject obj) where TModelFactory : class
        {
            if (obj == null) throw new ArgumentNullException("obj");

            // TODO: Move DefaultViewModelDescriptor back to DataType
            var t = typeof(CompoundObjectViewModel);
            //var t = obj.GetCompoundObjectDefinition(FrozenContext)
            //    .DefaultViewModelDescriptor
            //    .ViewModelRef
            //    .AsType(true);
            return CreateViewModel<TModelFactory>(ResolveFactory(t));
        }

        public TModelFactory CreateViewModel<TModelFactory>(Property p) where TModelFactory : class
        {
            if (p == null) { throw new ArgumentNullException("p"); }

            if (p.ValueModelDescriptor != null)
            {
                return CreateViewModel<TModelFactory>(p.ValueModelDescriptor);
            }
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for property: '{0}' of Type '{1}'", p, p.GetType()));
            }
        }

        // TODO: should use database backed decision tables
        public TModelFactory CreateViewModel<TModelFactory>(Method method) where TModelFactory : class
        {
            return CreateViewModel<TModelFactory>(ResolveFactory(typeof(ActionViewModel)));
        }

        // TODO: should use database backed decision tables
        public TModelFactory CreateViewModel<TModelFactory>(BaseParameter param) where TModelFactory : class
        {
            if (param == null) { throw new ArgumentNullException("param"); }
            Type t;
            if (param is BoolParameter && !param.IsList)
            {
                t = typeof(NullableBoolPropertyViewModel);
            }
            else if (param is DateTimeParameter && !param.IsList)
            {
                t = typeof(NullableDateTimePropertyViewModel);
            }
            else if (param is DoubleParameter && !param.IsList)
            {
                t = typeof(NullableStructValueViewModel<double>);
            }
            else if (param is IntParameter && !param.IsList)
            {
                t = typeof(NullableStructValueViewModel<int>);
            }
            else if (param is DecimalParameter && !param.IsList)
            {
                t = typeof(NullableStructValueViewModel<decimal>);
            }
            else if (param is StringParameter && !param.IsList)
            {
                t = typeof(ClassValueViewModel<string>);
            }
            else if (param is ObjectParameter && !param.IsList)
            {
                t = typeof(ObjectReferenceViewModel);
            }
            //else if (retParam is EnumParameter && !retParam.IsList)
            //{
            //    return (ModelFactory.CreateViewModel<NullableResultModel<?>.Factory>().Invoke(DataContext, _object, pm));
            //}
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for parameter '{0}' with type '{1}'", param, param.GetParameterType()));
            }

            return CreateViewModel<TModelFactory>(ResolveFactory(t));
        }

        public TModelFactory CreateViewModel<TModelFactory>(ViewModelDescriptor desc) where TModelFactory : class
        {
            if (desc == null) throw new ArgumentNullException("desc");
            return CreateViewModel<TModelFactory>(ResolveFactory(desc.ViewModelRef.AsType(true)));
        }

        private static int _resolveCounter = 0;
        private static int _resolveCompileCounter = 0;

        public TModelFactory CreateViewModel<TModelFactory>(Type t) where TModelFactory : class
        {
            var cacheKey = new VMCacheKey(t, typeof(TModelFactory));
            if (_viewModelFactoryCache.ContainsKey(cacheKey)) return (TModelFactory)_viewModelFactoryCache[cacheKey];

            if (t == null) throw new ArgumentNullException("t");
            // try to resolve factory if t is not such a factory
            if (!typeof(Delegate).IsAssignableFrom(t))
            {
                t = ResolveFactory(t);
                // throw an exception if t is still not a factory
                if (t == null || !typeof(Delegate).IsAssignableFrom(t)) throw new ArgumentOutOfRangeException("t", "Parameter must be a Delegate. CreateViewModel uses Autofac's Factory pattern");
            }
            try
            {
                if ((++_resolveCounter % 100) == 0)
                {
                    Logging.Log.WarnFormat("CreateViewModel was called {0} times", _resolveCounter);
                }

                var factory = Container.Resolve(t);
                if (t != typeof(TModelFactory))
                {

                    if ((++_resolveCompileCounter % 100) == 0)
                    {
                        Logging.Log.WarnFormat("CreateViewModel with compiling a lambda was called {0} times", _resolveCompileCounter);
                    }

                    // Wrap delegate. This will implement inheritance
                    Delegate factoryDelegate = (Delegate)factory;
                    // Get Parameter of both, src and dest delegate
                    var parameter_factory = factoryDelegate.Method.GetParameters().Skip(1).ToArray();
                    var parameter = typeof(TModelFactory).GetMethod("Invoke").GetParameters()
                        .Select(p => Expression.Parameter(p.ParameterType, p.Name)).ToArray();

                    // create cast expressions. Sometimes a UpCast is needed. 
                    // TModelFactory == DataTypeViewModel
                    // typeof(factory) == ObjectClassViewModel
                    // TODO: check that the parameter types can match at all
                    //       this is necessary to create proper errors when Factory delegates
                    //       are out of sync
                    var invoke = Expression.Invoke(Expression.Constant(factoryDelegate),
                        parameter.Select(
                            (p, idx) => Expression.Convert(p, parameter_factory[idx].ParameterType)
                            ).ToArray()
                        );
                    var l = Expression.Lambda(typeof(TModelFactory), invoke, parameter);
                    factory = l.Compile() as TModelFactory;
                }

                _viewModelFactoryCache[cacheKey] = factory;
                return (TModelFactory)factory;
            }
            catch (Exception ex)
            {
                Logging.Log.Error(string.Format("Unable to create type {0}", t.FullName), ex);
                throw;
            }
        }

        private Type ResolveFactory(Type t)
        {
            if (t == null) throw new ArgumentNullException("t");
            if (typeof(Delegate).IsAssignableFrom(t)) return t;
            Type f = null;
            while (t != null)
            {
                f = t.GetNestedType("Factory");
                if (f == null)
                {
                    Logging.Log.WarnFormat("Type [{0}] does not have a [Factory] delegate. Looking in base class", t.FullName);
                    t = t.BaseType;
                }
                else
                {
                    if (t.IsGenericType)
                    {
                        f = f.MakeGenericType(t.GetGenericArguments());
                    }
                    break;
                }
            }
            if (f == null) throw new InvalidOperationException(string.Format("The specified Type [{0}] does not contain a Factory", t.FullName));
            return f;
        }
        #endregion

        #region Top-Level Views Management

        /// <summary>
        /// Creates a default View for the given ViewModel.
        /// </summary>
        /// <param name="mdl">the model to be viewed</param>
        /// <returns>the configured view</returns>
        protected virtual object CreateDefaultView(ViewModel mdl)
        {
            if (mdl == null) { throw new ArgumentNullException("mdl"); }

            ViewModelDescriptor pmd = mdl
                .GetType()
                .ToRef(FrozenContext)
                .GetViewModelDescriptor();

            var vDesc = mdl.RequestedKind != null
                ? pmd.GetViewDescriptor(Toolkit, mdl.RequestedKind)
                : pmd.GetViewDescriptor(Toolkit);

            return CreateSpecificView(mdl, vDesc);
        }

        /// <summary>
        /// Creates a specific View for the given ViewModel.
        /// </summary>
        /// <param name="mdl">the model to be viewed</param>
        /// <param name="kind">the kind of view to create</param>
        /// <returns>the configured view</returns>
        protected virtual object CreateSpecificView(ViewModel mdl, ControlKind kind)
        {
            if (mdl == null) { throw new ArgumentNullException("mdl"); }
            if (kind == null) { throw new ArgumentNullException("kind"); }

            ViewModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext)
                .GetViewModelDescriptor();

            var vDesc = pmd.GetViewDescriptor(Toolkit, kind);

            return CreateSpecificView(mdl, vDesc);
        }

        /// <summary>
        /// Creates a specific View for the specified view model from the the specified view descriptor.
        /// In case of <see cref="WindowViewModel"/>s, only a single View is ever created for the specified mdl/vDesc.ControlKind pair.
        /// </summary>
        /// <remarks>
        /// WindowViews are currently expected to self-destruct when WindowViewModel.Show becomes false.
        /// Therefore we will also remove the model and its view from the cache if this happens.
        /// </remarks>
        /// <param name="mdl"></param>
        /// <param name="vDesc"></param>
        /// <returns>a newly created view, an existing view or null</returns>
        private object CreateSpecificView(ViewModel mdl, ViewDescriptor vDesc)
        {
            // no matching view found; aborting
            if (vDesc == null)
                return null;

            object result;
            var windowViewModel = mdl as WindowViewModel;
            if (windowViewModel != null)
            {
                windowViewModel.Show = true;
                Dictionary<ControlKind, object> views;
                if (_windowViews.TryGetValue(windowViewModel, out views))
                {
                    if (!views.TryGetValue(vDesc.ControlKind, out result))
                    {
                        result = CreateView(vDesc);
                        views[vDesc.ControlKind] = result;
                        InstallRemovalHandler(windowViewModel, vDesc.ControlKind);
                    }
                }
                else
                {
                    result = CreateView(vDesc);
                    _windowViews[windowViewModel] = new Dictionary<ControlKind, object>()
                    {
                        { vDesc.ControlKind, result }
                    };
                    InstallRemovalHandler(windowViewModel, vDesc.ControlKind);
                }
            }
            else
            {
                result = CreateView(vDesc);
            }

            return result;
        }

        private void InstallRemovalHandler(WindowViewModel windowViewModel, ControlKind controlKind)
        {
            PropertyChangedEventHandler handler = null;

            handler = (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == "Show" && !windowViewModel.Show)
                {
                    RemoveWindowViewModel(windowViewModel, controlKind);
                    windowViewModel.PropertyChanged -= handler;
                }
            };

            windowViewModel.PropertyChanged += handler;
        }

        private void RemoveWindowViewModel(WindowViewModel windowViewModel, ControlKind controlKind)
        {
            var views = _windowViews[windowViewModel];
            views.Remove(controlKind);
            if (views.Count == 0)
            {
                _windowViews.Remove(windowViewModel);
            }
        }

        private Dictionary<WindowViewModel, Dictionary<ControlKind, object>> _windowViews = new Dictionary<WindowViewModel, Dictionary<ControlKind, object>>();

        /// <summary>
        /// Creates a view from a view descriptor. By default it just creates a new instance of the ControlRef
        /// </summary>
        /// <param name="vDesc">the descriptor describing the view</param>
        /// <returns>a newly created view</returns>
        protected virtual object CreateView(ViewDescriptor vDesc)
        {
            return vDesc.ControlRef.Create();
        }

        public void ShowModel(ViewModel mdl, ControlKind kind, bool activate)
        {
            if (kind == null)
            {
                ShowModel(mdl, activate);
            }
            else
            {
                ShowInView(mdl, CreateSpecificView(mdl, kind), activate, false);
            }
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

            var dom = mdl as DataObjectViewModel;

            if (dom == null)
            {
                ShowInView(mdl, CreateDefaultView(mdl), activate, false);
            }
            else
            {
                IMultipleInstancesManager m;
                if (Managers.TryGetValue(dom.Object.Context, out m))
                {
                    m.AddItem(dom);
                    m.SelectedItem = dom;
                }
                else
                {
                    // TODO: notify user too
                    Logging.Client.ErrorFormat("Trying to open DataObjectViewModel without manager");
                }
            }
        }

        public bool CanShowModel(ViewModel mdl)
        {
            if (mdl == null)
                throw new ArgumentNullException("mdl");

            var dom = mdl as DataObjectViewModel;

            if (dom == null)
            {
                return true;
            }
            else
            {
                return Managers.ContainsKey(dom.Object.Context);
            }
        }

        public void ShowDialog(ViewModel mdl)
        {
            ShowDialog(mdl, null);
        }

        public void ShowDialog(ViewModel mdl, Kistl.App.GUI.ControlKind kind)
        {
            if (mdl == null)
                throw new ArgumentNullException("mdl");

            if (kind == null)
            {
                ShowInView(mdl, CreateDefaultView(mdl), true, true);
            }
            else
            {
                ShowInView(mdl, CreateSpecificView(mdl, kind), true, true);
            }
        }

        protected abstract void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog);

        #endregion

        #region Workspace Management

        protected Dictionary<IKistlContext, IMultipleInstancesManager> Managers { get; private set; }

        public virtual void OnIMultipleInstancesManagerCreated(IKistlContext ctx, IMultipleInstancesManager workspace)
        {
            this.Managers[ctx] = workspace;
        }
        public virtual void OnIMultipleInstancesManagerDisposed(IKistlContext ctx, IMultipleInstancesManager workspace)
        {
            this.Managers.Remove(ctx);
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
        /// <summary>
        /// Asks the user for a filename to save to.
        /// </summary>
        /// <param name="filename">A default filename or null or empty</param>
        /// <param name="filter">a list of filters for files to display in the format of "Label|glob1[;glob2[;...]"</param>
        /// <returns>the chosen file name or <code>String.Empty</code> if the user aborted the selection</returns>
        public abstract string GetDestinationFileNameFromUser(string filename, params string[] filter);

        public abstract bool GetDecisionFromUser(string message, string caption);

        public abstract void ShowMessage(string message, string caption);

        public void InitCulture()
        {
            if (Configuration.Client == null) return;
            if (!string.IsNullOrEmpty(Configuration.Client.Culture))
            {
                System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.GetCultureInfo(Configuration.Client.Culture);
            }
            if (!string.IsNullOrEmpty(Configuration.Client.UICulture))
            {
                System.Threading.Thread.CurrentThread.CurrentUICulture = System.Globalization.CultureInfo.GetCultureInfo(Configuration.Client.UICulture);
            }
        }

        #endregion

        public virtual IDelayedTask CreateDelayedTask(ViewModel displayer, Action loadAction)
        {
            return new ImmediateTask(loadAction);
        }

        /// <summary>
        /// Creates a delayed task using CreateDelayedTask and immediately triggers it.
        /// </summary>
        /// <param name="displayer"></param>
        /// <param name="loadAction"></param>
        public virtual void TriggerDelayedTask(ViewModel displayer, Action loadAction)
        {
            var task = CreateDelayedTask(displayer, loadAction);
            task.Trigger();
        }
    }
}
