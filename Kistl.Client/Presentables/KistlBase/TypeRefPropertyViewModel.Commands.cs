
namespace Kistl.Client.Presentables.KistlBase
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.Client.Models;

    /// <summary>
    /// A intermediate helper class to provide common "load TypeRefs from various Assembly sources" functionality.
    /// </summary>
    public abstract class LoadTypeRefCommand
        : CommandViewModel
    {
        /// <summary>
        /// Initializes a new instance of the LoadTypeRefCommand class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">a ViewModel which should be notified while this command is executing</param>
        /// <param name="label">a label for this command</param>
        /// <param name="tooltip">a tooltip for this command</param>
        /// <param name="parentType">where to put the Command's result</param>
        /// This constructor is internal to avoid external inheritance of this class.
        internal LoadTypeRefCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, string label, string tooltip, TypeRefPropertyViewModel parentType)
            : base(appCtx, dataCtx, parent, label, tooltip)
        {
            this.ParentType = parentType;
        }

        /// <summary>
        /// Gets the parent model, whose Value will be set by this command.
        /// </summary>
        protected TypeRefPropertyViewModel ParentType { get; private set; }

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
            string assemblyFileName = ViewModelFactory.GetSourceFileNameFromUser("Assembly files|*.dll;*.exe", "All files|*.*");
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
        /// Opens a dialog to let the user choose a type references from the specified assembly.
        /// </summary>
        /// <param name="assembly">the assembly to choose from</param>
        protected void ChooseTypeRefFromAssembly(Kistl.App.Base.Assembly assembly)
        {
            var regenerateCmd = ViewModelFactory.CreateViewModel<RegenerateTypeRefsCommand.Factory>().Invoke(DataContext, Parent, this);
            var selectionTask = ViewModelFactory.CreateViewModel<DataObjectSelectionTaskViewModel.Factory>().Invoke(
                DataContext,
                Parent,
                typeof(TypeRef).GetObjectClass(FrozenContext),
                () => DataContext.GetQuery<Kistl.App.Base.TypeRef>(),
                new Action<DataObjectViewModel>(delegate(DataObjectViewModel chosen)
                {
                    if (chosen != null)
                    {
                        this.ParentType.Value = chosen;
                    }
                }),
                new List<CommandViewModel>() { regenerateCmd }
            );
            //var filter = selectionTask.ListViewModel.Filter.Single(i => i.Property.ExportGuid == new Guid("885BFA97-3D43-48BB-A0AA-1049298714FF"));
            //filter.Value = filter.PossibleValues
            //    .Cast<KeyValuePair<DataObjectViewModel, string>>()
            //    .Where(m => m.Key != null)
            //    .FirstOrDefault(m => m.Key.Object == assembly)
            //    .Key;
            regenerateCmd.ListModel = selectionTask.ListViewModel;
            ViewModelFactory.ShowModel(selectionTask, true);
        }

        private class RegenerateTypeRefsCommand : CommandViewModel
        {
            public new delegate RegenerateTypeRefsCommand Factory(IKistlContext dataCtx, ViewModel parent, LoadTypeRefCommand outer);

            public InstanceListViewModel ListModel { get; set; }

            public RegenerateTypeRefsCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent)
                : base(appCtx, dataCtx, parent, "Regenerate TypeRefs", "Regenerate TypeRefs")
            {
            }

            private Assembly FetchAssembly()
            {
                return null;
                //var filter = (ObjectReferencePropertyFilterExpressionViewModel)ListModel.Filter.OfType<IPropertyFilterExpression>().Single(i => i.Property.ExportGuid == new Guid("885BFA97-3D43-48BB-A0AA-1049298714FF"));
                //if (filter.Value == null) return null;
                //return (Assembly)filter.Value.Object;
            }

            public override bool CanExecute(object data)
            {
                return FetchAssembly() != null;
            }

            protected override void DoExecute(object data)
            {
                var a = FetchAssembly();
                if (a != null)
                {
                    a.RegenerateTypeRefs();
                    ListModel.ReloadInstances();
                }
            }
        }

    }

    /// <summary>
    /// Loads or creates a TypeRef from the result of the user browsing an assembly file (.dll or .exe). Parameter must be <code>null</code> or a <see cref="Kistl.App.Base.Assembly"/> or a <see cref="System.Reflection.Assembly"/>.
    /// </summary>
    internal class LoadTypeRefFromAssemblyFileCommand
        : LoadTypeRefCommand
    {
        public new delegate LoadTypeRefFromAssemblyFileCommand Factory(IKistlContext dataCtx, ViewModel parent, TypeRefPropertyViewModel parentType);

        /// <summary>
        /// Initializes a new instance of the LoadTypeRefFromAssemblyFileCommand class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">a ViewModel which should be notified while this command is executing</param>
        /// <param name="parentType">where to put the Command's result</param>
        /// This constructor is internal to avoid external inheritance of this class.
        public LoadTypeRefFromAssemblyFileCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, TypeRefPropertyViewModel parentType)
            : base(appCtx, dataCtx, parent, "Load TypeRef From DLL", "Loads all types from a DLL", parentType)
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
        public new delegate LoadTypeRefFromAssemblyRefCommand Factory(IKistlContext dataCtx, ViewModel parent, TypeRefPropertyViewModel parentType);

        /// <summary>
        /// Initializes a new instance of the LoadTypeRefFromAssemblyRefCommand class.
        /// </summary>
        /// <param name="appCtx">the application context to use</param>
        /// <param name="dataCtx">the data context to use</param>
        /// <param name="parent">a ViewModel which should be notified while this command is executing</param>
        /// <param name="parentType">where to put the Command's result</param>
        /// This constructor is internal to avoid external inheritance of this class.
        public LoadTypeRefFromAssemblyRefCommand(IViewModelDependencies appCtx, IKistlContext dataCtx, ViewModel parent, TypeRefPropertyViewModel parentType)
            : base(appCtx, dataCtx, parent, "Select TypeRef by Assembly", "Loads all types from an Assembly", parentType)
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
            ChooseTypeRefFromAssembly(assembly);
        }
    }
}