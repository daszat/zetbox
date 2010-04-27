
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;

    /// <summary>
    /// A intermediate helper class to provide common "load TypeRefs from various Assembly sources" functionality.
    /// </summary>
    public abstract class LoadTypeRefCommand
        : CommandModel
    {
        /// <summary>
        /// Initializes a new instance of the LoadTypeRefCommand class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="label">a label for this command</param>
        /// <param name="tooltip">a tooltip for this command</param>
        /// <param name="parent">where to put the Command's result</param>
        /// This constructor is internal to avoid external inheritance of this class.
        internal LoadTypeRefCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, string label, string tooltip, TypeRefPropertyModel parent)
            : base(appCtx, dataCtx, label, tooltip)
        {
            this.Parent = parent;
        }

        /// <summary>
        /// Gets the parent model, whose Value will be set by this command.
        /// </summary>
        protected TypeRefPropertyModel Parent { get; private set; }

        /// <summary>
        /// Loads a <see cref="System.Reflection.Assembly"/> from the specified <see cref="Kistl.App.Base.Assembly"/>.
        /// </summary>
        /// <param name="kistlAssembly">the assembly to load</param>
        /// <returns>a system assembly loaded in the reflection only application context.</returns>
        protected System.Reflection.Assembly ReflectionFromRef(Kistl.App.Base.Assembly kistlAssembly)
        {
            return System.Reflection.Assembly.ReflectionOnlyLoad(kistlAssembly.Name);
        }

        /// <summary>
        /// Loads or creates a <see cref="Kistl.App.Base.Assembly"/> for the specified <see cref="System.Reflection.Assembly"/>.
        /// </summary>
        /// <param name="systemAssembly">the assembly to load</param>
        /// <returns>a system assembly loaded in the reflection only application context.</returns>
        protected Kistl.App.Base.Assembly RefFromReflection(System.Reflection.Assembly systemAssembly)
        {
            var assemblyDescriptor = DataContext.GetQuery<Assembly>().SingleOrDefault(a => a.Name == systemAssembly.FullName);
            if (assemblyDescriptor == null)
            {
                assemblyDescriptor = DataContext.Create<Assembly>();
                assemblyDescriptor.Name = systemAssembly.FullName;
            }

            return assemblyDescriptor;
        }

        /// <summary>
        /// Loads a <see cref="System.Reflection.Assembly"/> from a file which is specified by the user.
        /// </summary>
        /// <returns>a system assembly loaded in the reflection only application context.</returns>
        protected System.Reflection.Assembly ReflectionFromUser()
        {
            string assemblyFileName = ModelFactory.GetSourceFileNameFromUser("Assembly files|*.dll;*.exe", "All files|*.*");
            if (String.IsNullOrEmpty(assemblyFileName))
            {
                return null;
            }
            else
            {
                return System.Reflection.Assembly.ReflectionOnlyLoadFrom(assemblyFileName);
            }
        }

        /// <summary>
        /// Opens a dialog to let the user choose an assembly and the type references from that assembly.
        /// </summary>
        protected void ChooseAssemblyAndTypeRef()
        {
            var selectionTask = ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                DataContext,
                DataContext.GetQuery<Kistl.App.Base.Assembly>()
                    .OrderBy(a => a.Name)
                    .Select(a => ModelFactory.CreateViewModel<DataObjectModel.Factory>(a).Invoke(DataContext, a))
                    .ToList(),
                new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                {
                    if (chosen != null)
                    {
                        ChooseTypeRefFromAssembly(chosen.Object as Kistl.App.Base.Assembly);
                    }
                }),
                null);
            ModelFactory.ShowModel(selectionTask, true);
        }

        /// <summary>
        /// Opens a dialog to let the user choose a type references from the specified assembly.
        /// </summary>
        /// <param name="assembly">the assembly to choose from</param>
        protected void ChooseTypeRefFromAssembly(Kistl.App.Base.Assembly assembly)
        {
            var selectionTask = ModelFactory.CreateViewModel<DataObjectSelectionTaskModel.Factory>().Invoke(
                DataContext,
                GetTypeRefList(assembly),
                new Action<DataObjectModel>(delegate(DataObjectModel chosen)
                {
                    if (chosen != null)
                    {
                        this.Parent.Value = chosen;
                    }
                }),
                new List<CommandModel>() { ModelFactory.CreateViewModel<RegenerateTypeRefsCommand.Factory>().Invoke(DataContext, this, assembly) });
            ModelFactory.ShowModel(selectionTask, true);
        }

        internal List<DataObjectModel> GetTypeRefList(Kistl.App.Base.Assembly assembly)
        {
            return DataContext.GetQuery<Kistl.App.Base.TypeRef>().Where(tr => tr.Assembly.ID == assembly.ID)
                                .ToList()
                                .OrderBy(tr => tr.FullName)
                                .Select(tr => ModelFactory.CreateViewModel<DataObjectModel.Factory>(tr).Invoke(DataContext, tr))
                                .ToList();
        }

        private class RegenerateTypeRefsCommand : CommandModel
        {
            public new delegate RegenerateTypeRefsCommand Factory(IKistlContext dataCtx, LoadTypeRefCommand outer, Kistl.App.Base.Assembly assembly);

            private Kistl.App.Base.Assembly _assembly;
            private LoadTypeRefCommand _outer;

            public RegenerateTypeRefsCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, LoadTypeRefCommand outer, Kistl.App.Base.Assembly assembly)
                : base(appCtx, dataCtx, "Regenerate TypeRefs", "Regenerate TypeRefs")
            {
                _outer = outer;
                _assembly = assembly;
            }

            public override bool CanExecute(object data)
            {
                return data is DataObjectSelectionTaskModel;
            }

            protected override void DoExecute(object data)
            {
                _assembly.RegenerateTypeRefs();
                var mdl = (DataObjectSelectionTaskModel)data;
                mdl.Refresh(_outer.GetTypeRefList(_assembly));
            }
        }

    }

    /// <summary>
    /// Loads or creates a TypeRef from the result of the user browsing an assembly file (.dll or .exe). Parameter must be <code>null</code> or a <see cref="Kistl.App.Base.Assembly"/> or a <see cref="System.Reflection.Assembly"/>.
    /// </summary>
    internal class LoadTypeRefFromAssemblyFileCommand
        : LoadTypeRefCommand
    {
        public new delegate LoadTypeRefFromAssemblyFileCommand Factory(IKistlContext dataCtx, TypeRefPropertyModel parent);

        /// <summary>
        /// Initializes a new instance of the LoadTypeRefFromAssemblyFileCommand class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">where to put the Command's result</param>
        /// This constructor is internal to avoid external inheritance of this class.
        public LoadTypeRefFromAssemblyFileCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, TypeRefPropertyModel parent)
            : base(appCtx, dataCtx, "Load TypeRef From DLL", "Loads all types from a DLL", parent)
        {
        }

        /// <summary>
        /// Checks whether the parameter is valid.
        /// The parameter must be <code>null</code> or a <see cref="System.Reflection.Assembly"/>.
        /// </summary>
        /// <param name="data"><code>null</code> or a <see cref="System.Reflection.Assembly"/></param>
        /// <returns>true if the constraints are fulfilled</returns>
        public override bool CanExecute(object data)
        {
            if (data == null)
                return true;

            if (data is System.Reflection.Assembly)
                return true;

            return false;
        }

        /// <summary>
        /// Choose a 
        /// </summary>
        /// <param name="data"></param>
        protected override void DoExecute(object data)
        {
            if (!CanExecute(data))
                return;

            var assembly = data as System.Reflection.Assembly ?? this.ReflectionFromUser();
            if (assembly != null)
            {
                ChooseTypeRefFromAssembly(RefFromReflection(assembly));
            }
        }
    }

    /// <summary>
    /// Loads or creates a TypeRef from the result of the user browsing an assembly.
    /// Parameter must be <code>null</code> or a <see cref="Kistl.App.Base.Assembly"/>.
    /// </summary>
    internal class LoadTypeRefFromAssemblyRefCommand
        : LoadTypeRefCommand
    {
        public new delegate LoadTypeRefFromAssemblyRefCommand Factory(IKistlContext dataCtx, TypeRefPropertyModel parent);

        /// <summary>
        /// Initializes a new instance of the LoadTypeRefFromAssemblyRefCommand class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">where to put the Command's result</param>
        /// This constructor is internal to avoid external inheritance of this class.
        public LoadTypeRefFromAssemblyRefCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, TypeRefPropertyModel parent)
            : base(appCtx, dataCtx, "Select TypeRef by Assembly", "Loads all types from an Assembly", parent)
        {
        }

        /// <summary>
        /// Checks whether the parameter is valid. The parameter must be <code>null</code> or a <see cref="Kistl.App.Base.Assembly"/>.
        /// </summary>
        /// <param name="data"><code>null</code> or a <see cref="Kistl.App.Base.Assembly"/></param>
        /// <returns>true if the constraints are fulfilled</returns>
        public override bool CanExecute(object data)
        {
            if (data == null)
                return true;

            if (data is Kistl.App.Base.Assembly)
                return true;

            return false;
        }

        protected override void DoExecute(object data)
        {
            if (!CanExecute(data))
                return;

            var assembly = data as Kistl.App.Base.Assembly;
            if (assembly == null)
            {
                this.ChooseAssemblyAndTypeRef();
            }
            else
            {
                ChooseTypeRefFromAssembly(assembly);
            }
        }
    }
}