using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Kistl.Client.Presentables;

namespace Kistl.Client.ASPNET.Toolkit
{
    [ServiceContract(Namespace = "Kistl.Client.ASPNET")] // Client side namespace
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WorkspaceModelService
    {
        [OperationContract]
        public List<JavaScriptObjectMoniker> GetModules()
        {
            var workspace = GuiApplicationContext.Current.Factory
                .CreateSpecificModel<WorkspaceModel>(KistlContextManagerModule.KistlContext);

            return workspace.Modules.Select(i => new JavaScriptObjectMoniker(i.Object)).ToList();
        }

        [OperationContract]
        public List<JavaScriptObjectMoniker> GetObjectClasses(int moduleID)
        {
            var workspace = GuiApplicationContext.Current.Factory
                .CreateSpecificModel<WorkspaceModel>(KistlContextManagerModule.KistlContext);

            return workspace.Modules.Single(m => m.ID == moduleID).ObjectClasses
                .Select(i => new JavaScriptObjectMoniker(i.Object)).ToList();
        }
    }
}
