namespace Kistl.Client.Presentables
{
    using System;

    public interface IModelFactory
    {
        PresentableModel CreateDefaultModel(Kistl.API.IKistlContext ctx, Kistl.API.IDataObject obj, params object[] data);
        object CreateDefaultView(PresentableModel mdl);
        PresentableModel CreateModel(Type requestedType, Kistl.API.IKistlContext ctx, object[] data);
        PresentableModel CreatePropertyValueModel(Kistl.API.IKistlContext ctx, Kistl.API.IDataObject obj, Kistl.App.Base.Property p);
        TModel CreateSpecificModel<TModel>(Kistl.API.IKistlContext ctx, params object[] data) where TModel : PresentableModel;
        object CreateSpecificView(PresentableModel mdl, Kistl.App.GUI.ControlKind kind);
        void CreateTimer(TimeSpan tickLength, Action action);
        string GetSourceFileNameFromUser(params string[] filter);
        void ShowModel(PresentableModel mdl, bool activate);
        void ShowModel(PresentableModel mdl, Kistl.App.GUI.ControlKind kind, bool activate);
    }
}
