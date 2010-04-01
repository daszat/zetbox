namespace Kistl.Client.Presentables
{
    using System;

    public interface IModelFactory
    {
        ViewModel CreateDefaultModel(Kistl.API.IKistlContext ctx, Kistl.API.IDataObject obj, params object[] data);
        object CreateDefaultView(ViewModel mdl);
        ViewModel CreateModel(Type requestedType, Kistl.API.IKistlContext ctx, object[] data);
        ViewModel CreatePropertyValueModel(Kistl.API.IKistlContext ctx, Kistl.API.IDataObject obj, Kistl.App.Base.Property p);
        TModel CreateSpecificModel<TModel>(Kistl.API.IKistlContext ctx, params object[] data) where TModel : ViewModel;
        object CreateSpecificView(ViewModel mdl, Kistl.App.GUI.ControlKind kind);
        void CreateTimer(TimeSpan tickLength, Action action);
        string GetSourceFileNameFromUser(params string[] filter);
        void ShowModel(ViewModel mdl, bool activate);
        void ShowModel(ViewModel mdl, Kistl.App.GUI.ControlKind kind, bool activate);
    }
}
