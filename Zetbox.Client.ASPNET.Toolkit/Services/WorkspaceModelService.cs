using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;

using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.App.Base;
using Zetbox.App.Extensions;
using Zetbox.Client.Presentables;
using Zetbox.Client.Presentables.ObjectBrowser;

namespace Zetbox.Client.ASPNET.Toolkit
{
    [ServiceContract(Namespace = "Zetbox.Client.ASPNET")] // Client side namespace
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class WorkspaceModelService
    {
        [OperationContract]
        public List<JavaScriptObjectMoniker> GetModules()
        {
            var workspace = ZetboxContextManagerModule.ViewModelFactory
                .CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ZetboxContextManagerModule.ZetboxContext, null);

            return workspace.Modules.Select(i => new JavaScriptObjectMoniker(i)).ToList();
        }

        [OperationContract]
        public List<JavaScriptObjectMoniker> GetObjectClasses(int moduleID)
        {
            var workspace = ZetboxContextManagerModule.ViewModelFactory
                .CreateViewModel<WorkspaceViewModel.Factory>().Invoke(ZetboxContextManagerModule.ZetboxContext, null);

            return workspace.Modules.Single(m => m.ID == moduleID).ObjectClasses
                .Select(i => new JavaScriptObjectMoniker(i.DataTypeViewModel)).ToList();
        }

        [OperationContract]
        public List<JavaScriptObjectMoniker> GetInstances(int objectClassID)
        {
            throw new NotImplementedException();
        //    // Dont use model - directly selecting is faster
        //    var objClass = ZetboxContextManagerModule.ZetboxContext.Find<ObjectClass>(objectClassID);
        //    return ZetboxContextManagerModule.ZetboxContext.GetQuery(objClass.GetDescribedInterfaceType())
        //            .Select(i => new JavaScriptObjectMoniker(ZetboxContextManagerModule.ZetboxContext, i)).ToList();
        }
    }
}
