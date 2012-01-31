
namespace Kistl.Client.Presentables
{
    using System;
    using Kistl.API;
    using Kistl.API.Client;
    using Kistl.API.Client.PerfCounter;
    using Kistl.App.GUI;

    public interface IViewModelFactory : IToolkit
    {
        void ShowModel(ViewModel mdl, bool activate);
        void ShowModel(ViewModel mdl, Kistl.App.GUI.ControlKind kind, bool activate);

        void ShowDialog(ViewModel mdl);
        void ShowDialog(ViewModel mdl, Kistl.App.GUI.ControlKind kind);

        bool CanShowModel(ViewModel mdl);

        ViewModel GetWorkspace(IKistlContext ctx);

        /// <summary>
        /// Initializes the requested culture from config on the current thread
        /// </summary>
        void InitCulture();

        // Create Models
        TModelFactory CreateViewModel<TModelFactory>() where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.API.IDataObject obj) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.API.ICompoundObject obj) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.App.Base.Property p) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.App.Base.BaseParameter p) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.App.Base.Method m) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(ViewModelDescriptor desc) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(System.Type t) where TModelFactory : class;

        IDelayedTask CreateDelayedTask(ViewModel displayer, Action loadAction);
        void TriggerDelayedTask(ViewModel displayer, Action loadAction);

        // IMultipleInstancesManager
        void OnIMultipleInstancesManagerCreated(Kistl.API.IKistlContext ctx, IMultipleInstancesManager workspace);
        void OnIMultipleInstancesManagerDisposed(Kistl.API.IKistlContext ctx, IMultipleInstancesManager workspace);

        IPerfCounter PerfCounter { get; }
    }
}
