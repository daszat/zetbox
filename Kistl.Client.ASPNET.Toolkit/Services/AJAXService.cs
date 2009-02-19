using System;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Collections.Generic;
using Kistl.Client.ASPNET.Toolkit;
using Kistl.API;
using Kistl.API.Client;

namespace Kistl.Client.ASPNET.Toolkit
{
    [ServiceContract(Namespace = "Kistl.Client.ASPNET")] // Client side namespace
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AJAXService
    {
        [OperationContract]
        public List<JavaScriptObjectMoniker> GetList(SerializableType type)
        {
            using (IKistlContext ctx = KistlContext.GetContext())
            {
                return ctx.GetQuery(type.GetSystemType())
                    .Select(i => new JavaScriptObjectMoniker(ctx, i)).ToList();
            }
        }
    }
}