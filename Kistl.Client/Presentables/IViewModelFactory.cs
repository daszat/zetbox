
namespace Kistl.Client.Presentables
{
    using System;
    using Kistl.App.GUI;

    public interface IViewModelFactory
    {
        void ShowModel(ViewModel mdl, bool activate);
        void ShowModel(ViewModel mdl, Kistl.App.GUI.ControlKind kind, bool activate);

        void ShowDialog(ViewModel mdl);
        void ShowDialog(ViewModel mdl, Kistl.App.GUI.ControlKind kind);

        void CreateTimer(TimeSpan tickLength, Action action);
        string GetSourceFileNameFromUser(params string[] filter);
        string GetDestinationFileNameFromUser(string filename, params string[] filter);
        bool GetDecisionFromUser(string message, string caption);
        void ShowMessage(string message, string caption);

        Toolkit Toolkit { get; }

        // Create Models
        TModelFactory CreateViewModel<TModelFactory>() where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.API.IDataObject obj) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.API.ICompoundObject obj) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.App.Base.Property p) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.App.Base.BaseParameter p) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(Kistl.App.Base.Method m) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(ViewModelDescriptor desc) where TModelFactory : class;
        TModelFactory CreateViewModel<TModelFactory>(System.Type t) where TModelFactory : class;

        // IMultipleInstancesManager
        void OnIMultipleInstancesManagerCreated(Kistl.API.IKistlContext ctx, IMultipleInstancesManager workspace);
        void OnIMultipleInstancesManagerDisposed(Kistl.API.IKistlContext ctx, IMultipleInstancesManager workspace);
    }
}
