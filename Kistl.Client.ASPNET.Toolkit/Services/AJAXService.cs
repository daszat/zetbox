using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using Kistl.API;
using Kistl.API.Client;
using Kistl.Client.ASPNET.Toolkit;

namespace Kistl.Client.ASPNET.Toolkit
{
    [ServiceContract(Namespace = "Kistl.Client.ASPNET")] // Client side namespace
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AJAXService
    {
        [OperationContract]
        public List<JavaScriptObjectMoniker> GetList(SerializableType type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            throw new NotImplementedException();
            //try
            //{
            //    return KistlContextManagerModule.KistlContext.GetQuery(KistlContextManagerModule.IftFactory(type.GetSystemType()))
            //        .Select(i => new JavaScriptObjectMoniker(KistlContextManagerModule.KistlContext, i)).ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }
    }
}