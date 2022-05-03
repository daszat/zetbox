// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Client;
    using Zetbox.API.Client.PerfCounter;
    using Zetbox.API.Configuration;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;

    class LifetimeScopeFactory : ILifetimeScopeFactory
    {
        private readonly ILifetimeScope _scope;
        public LifetimeScopeFactory(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public ILifetimeScope BeginLifetimeScope()
        {
            return _scope.BeginLifetimeScope();
        }
    }

    class ViewModelFactoryScope : IViewModelFactoryScope
    {
        public ViewModelFactoryScope(ILifetimeScope scope, IViewModelFactory f)
        {
            if (scope == null) throw new ArgumentNullException("scope");
            if (f == null) throw new ArgumentNullException("f");
            this.Scope = scope;
            this.ViewModelFactory = f;
        }

        public ILifetimeScope Scope
        {
            get;
            private set;
        }

        public IViewModelFactory ViewModelFactory
        {
            get;
            private set;
        }

        public void Dispose()
        {
            Scope.Dispose();
        }
    }

    /// <summary>
    /// Abstract base class to provide basic functionality of all model factories. Toolkit-specific implementations of this class will be 
    /// used by the rendering infrastructure to create ViewModels and Views.
    /// </summary>
    public abstract class ViewModelFactory : IViewModelFactory
    {
        /// <summary>
        /// Gets the Toolkit of the implementation. A constant.
        /// </summary>
        public abstract Toolkit Toolkit { get; }

        protected readonly Autofac.ILifetimeScope Scope;
        protected readonly ILifetimeScopeFactory ScopeFactory;
        protected readonly IFrozenContext FrozenContext;
        protected readonly ZetboxConfig Configuration;
        protected readonly DialogCreator.Factory DialogFactory;

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

        protected ViewModelFactory(ILifetimeScopeFactory scopeFactory, Autofac.ILifetimeScope scope, IFrozenContext frozenCtx, ZetboxConfig cfg, IPerfCounter perfCounter, DialogCreator.Factory dialogFactory)
        {
            if (scopeFactory == null) throw new ArgumentNullException("scopeFactory");
            if (scope == null) throw new ArgumentNullException("scope");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            if (cfg == null) throw new ArgumentNullException("cfg");
            if (dialogFactory == null) throw new ArgumentNullException("dialogFactory");

            this.ScopeFactory = scopeFactory;
            this.Scope = scope;
            this.FrozenContext = frozenCtx;
            this.Configuration = cfg;
            this.Managers = new Dictionary<IZetboxContext, IMultipleInstancesManager>();
            this._viewModelFactoryCache = new Dictionary<VMCacheKey, object>();
            this.PerfCounter = perfCounter;
            this.DialogFactory = dialogFactory;
        }

        #region Model Management
        public TModelFactory CreateViewModel<TModelFactory>() where TModelFactory : class
        {
            return CreateViewModel<TModelFactory>(typeof(TModelFactory));
        }

        public TModelFactory CreateViewModel<TModelFactory>(IDataObject obj) where TModelFactory : class
        {
            if (obj == null) throw new ArgumentNullException("obj");

            var desc = obj.GetObjectClass(FrozenContext).GetProp_DefaultViewModelDescriptor().Result;
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

            var desc = obj.GetCompoundObjectDefinition(FrozenContext).GetProp_DefaultViewModelDescriptor().Result;
            var t = desc != null
                ? Type.GetType(desc.ViewModelTypeRef, true)
                : typeof(CompoundObjectViewModel);
            return CreateViewModel<TModelFactory>(ResolveFactory(t));
        }

        private class WorkaroundStringListViewModel : BaseValueViewModel
        {
            public new delegate WorkaroundStringListViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Zetbox.Client.Models.IValueModel mdl);

            public WorkaroundStringListViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, Zetbox.Client.Models.IValueModel mdl)
                : base(dependencies, dataCtx, parent, mdl)
            {
            }

            public override ControlKind RequestedKind
            {
                get
                {
                    return NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_MultiLineTextboxKind.Find(FrozenContext);
                }
                set
                {
                    base.RequestedKind = value;
                }
            }

            public override void Focus()
            {
            }

            public override void Blur()
            {
            }

            public override bool HasValue
            {
                get { return true; }
            }

            public override string FormattedValue
            {
                get
                {
                    return string.Empty;
                }
                set
                {
                    throw new NotImplementedException();
                }
            }

            public override string FormattedValueAsync
            {
                get { return FormattedValue; }
            }
        }

        public TModelFactory CreateViewModel<TModelFactory>(Property p) where TModelFactory : class
        {
            if (p == null) { throw new ArgumentNullException("p"); }

            // TODO: Bad workaround for string list properties
            var sp = p as StringProperty;
            if (sp != null && sp.IsList)
            {
                return CreateViewModel<TModelFactory>(ResolveFactory(typeof(WorkaroundStringListViewModel.Factory)));
            }
            else if (p.ValueModelDescriptor != null)
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
            var isList = param.IsList;
            Type t;
            if (param is BoolParameter && !isList)
            {
                t = typeof(NullableBoolPropertyViewModel);
            }
            else if (param is DateTimeParameter && !isList)
            {
                t = typeof(NullableDateTimePropertyViewModel);
            }
            else if (param is DoubleParameter && !isList)
            {
                t = typeof(NullableDoublePropertyViewModel);
            }
            else if (param is IntParameter && !isList)
            {
                t = typeof(NullableIntPropertyViewModel);
            }
            else if (param is DecimalParameter && !isList)
            {
                t = typeof(NullableDecimalPropertyViewModel);
            }
            else if (param is StringParameter && !isList)
            {
                t = typeof(StringValueViewModel);
            }
            else if (param is ObjectReferenceParameter && !isList)
            {
                t = typeof(ObjectReferenceViewModel);
            }
            else if (param is EnumParameter && !isList)
            {
                t = typeof(EnumerationValueViewModel);
            }
            else if (param is CompoundObjectParameter && !isList)
            {
                var compObj = ((CompoundObjectParameter)param).CompoundObject;
                if (compObj.DefaultPropertyViewModelDescriptor != null)
                {
                    t = Type.GetType(compObj.DefaultPropertyViewModelDescriptor.ViewModelTypeRef, true);
                }
                else
                {
                    t = typeof(CompoundObjectPropertyViewModel);
                }
            }
            else
            {
                throw new NotImplementedException(String.Format("==>> No model for parameter '{0}' with type '{1}'", param, param.GetParameterTypeString()));
            }

            return CreateViewModel<TModelFactory>(ResolveFactory(t));
        }

        public TModelFactory CreateViewModel<TModelFactory>(ViewModelDescriptor desc) where TModelFactory : class
        {
            if (desc == null) throw new ArgumentNullException("desc");
            return CreateViewModel<TModelFactory>(ResolveFactory(Type.GetType(desc.ViewModelTypeRef, true)));
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

                var factory = Scope.Resolve(t);
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
        protected virtual async Task<object> CreateDefaultView(ViewModel mdl)
        {
            if (mdl == null) { throw new ArgumentNullException("mdl"); }

            var pmd = GuiExtensions.GetViewModelDescriptor(mdl, FrozenContext);

            if (pmd == null) return null;

            var vDesc = mdl.RequestedKind != null
                ? await pmd.GetViewDescriptor(Toolkit, mdl.RequestedKind)
                : await pmd.GetViewDescriptor(Toolkit);

            return CreateSpecificView(mdl, vDesc);
        }

        /// <summary>
        /// Creates a specific View for the given ViewModel.
        /// </summary>
        /// <param name="mdl">the model to be viewed</param>
        /// <param name="kind">the kind of view to create</param>
        /// <returns>the configured view</returns>
        protected virtual async Task<object> CreateSpecificView(ViewModel mdl, ControlKind kind)
        {
            if (mdl == null) { throw new ArgumentNullException("mdl"); }
            if (kind == null) { throw new ArgumentNullException("kind"); }

            var pmd = GuiExtensions.GetViewModelDescriptor(mdl, FrozenContext);

            if (pmd == null) return null;

            return CreateSpecificView(mdl, await pmd.GetViewDescriptor(Toolkit, kind));
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
            return Activator.CreateInstance(Type.GetType(vDesc.ControlTypeRef));
        }

        public async Task ShowModel(ViewModel mdl, ControlKind kind, bool activate)
        {
            if (kind == null)
            {
                await ShowModel(mdl, activate);
            }
            else
            {
                ShowInView(mdl, await CreateSpecificView(mdl, kind), activate, false, null);
            }
        }

        /// <summary>
        /// Shows the specified model. 
        /// </summary>
        /// <param name="mdl"></param>
        /// <param name="activate"></param>
        public async Task ShowModel(ViewModel mdl, bool activate)
        {
            if (mdl == null)
                throw new ArgumentNullException("mdl");

            var dom = mdl as DataObjectViewModel;

            if (dom == null)
            {
                ShowInView(mdl, await CreateDefaultView(mdl), activate, false, null);
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

        public ViewModel GetWorkspace(IZetboxContext ctx)
        {
            if (Managers.ContainsKey(ctx))
            {
                return Managers[ctx] as ViewModel;
            }
            else
            {
                return null;
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
                if (dom.Object == null || dom.Object.Context == null)
                {
                    // Invalid object, like a deleted one
                    return false;
                }
                return Managers.ContainsKey(dom.Object.Context);
            }
        }

        public async Task ShowDialog(ViewModel mdl, ViewModel ownerModel, Zetbox.App.GUI.ControlKind kind = null)
        {
            if (mdl == null)
                throw new ArgumentNullException("mdl");

            if (kind == null)
            {
                ShowInView(mdl, await CreateDefaultView(mdl), true, true, ownerModel ?? mdl.GetWorkspace());
            }
            else
            {
                ShowInView(mdl, await CreateSpecificView(mdl, kind), true, true, ownerModel ?? mdl.GetWorkspace());
            }
        }

        protected abstract void ShowInView(ViewModel mdl, object view, bool activate, bool asDialog, ViewModel ownerModel);

        #endregion

        #region Workspace Management

        protected Dictionary<IZetboxContext, IMultipleInstancesManager> Managers { get; private set; }

        public virtual void OnIMultipleInstancesManagerCreated(IZetboxContext ctx, IMultipleInstancesManager workspace)
        {
            this.Managers[ctx] = workspace;
        }
        public virtual void OnIMultipleInstancesManagerDisposed(IZetboxContext ctx, IMultipleInstancesManager workspace)
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

        #region delayed tasks

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

        #endregion

        public IPerfCounter PerfCounter
        {
            get;
            private set;
        }

        public DialogCreator CreateDialog(IZetboxContext ctx, string title)
        {
            var result = DialogFactory(ctx);
            result.Title = title;
            return result;
        }

        #region Scope Management
        public IViewModelFactoryScope CreateNewScope()
        {
            var scope = ScopeFactory.BeginLifetimeScope();
            var vmf = scope.Resolve<IViewModelFactory>();
            return new ViewModelFactoryScope(scope, vmf);
        }

        public IZetboxContext CreateNewContext(ContextIsolationLevel isolationLevel = ContextIsolationLevel.PreferContextCache, ILifetimeScope scope = null)
        {
            var factory = (scope ?? Scope).Resolve<Func<ContextIsolationLevel, IZetboxContext>>();
            return factory(isolationLevel);
        }
        #endregion
    }
}
