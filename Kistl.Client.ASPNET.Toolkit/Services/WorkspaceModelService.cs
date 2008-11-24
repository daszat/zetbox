using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Kistl.Client.Presentables;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client.ASPNET.Toolkit
{
    [ServiceContract(Namespace = "Kistl.Client.ASPNET")] // Client side namespace
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WorkspaceModelService
    {
        [OperationContract]
        public List<JavaScriptObjectMoniker> GetModules()
        {
            // Das Model ist viel zu langsam
            //var workspace = GuiApplicationContext.Current.Factory
            //    .CreateSpecificModel<WorkspaceModel>(KistlContextManagerModule.KistlContext);

            //return workspace.Modules.Select(i => new JavaScriptObjectMoniker(i.Object)).ToList();
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                return ctx.GetQuery<Kistl.App.Base.Module>()
                    .Select(i => new JavaScriptObjectMoniker(i)).ToList();
            }
        }

        [OperationContract]
        public List<JavaScriptObjectMoniker> GetObjectClasses(int moduleID)
        {
            //var workspace = GuiApplicationContext.Current.Factory
            //    .CreateSpecificModel<WorkspaceModel>(KistlContextManagerModule.KistlContext);

            //return workspace.Modules.Single(m => m.ID == moduleID).ObjectClasses
            //    .Select(i => new JavaScriptObjectMoniker(i.Object)).ToList();
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                return ctx.GetQuery<Kistl.App.Base.ObjectClass>().Where(obj => obj.Module.ID == moduleID)
                    .Select(i => new JavaScriptObjectMoniker(i)).ToList();
            }
        }

        [OperationContract]
        public List<JavaScriptObjectMoniker> GetInstances(int objectClassID)
        {
            // TODO: Das geht so nicht mit dem Model
            // Jetzt selbst implementieren
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                var objClass = ctx.Find<Kistl.App.Base.ObjectClass>(objectClassID);
                return ctx.GetQuery(objClass.GetDataType())
                    .Select(i => new JavaScriptObjectMoniker(i)).ToList();
            }
        }
    }
}
