using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.Client.ASPNET.Toolkit;

namespace Zetbox.Client.ASPNET.Toolkit
{
    [ServiceContract(Namespace = "Zetbox.Client.ASPNET")] // Client side namespace
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
            //    return ZetboxContextManagerModule.ZetboxContext.GetQuery(ZetboxContextManagerModule.IftFactory(type.GetSystemType()))
            //        .Select(i => new JavaScriptObjectMoniker(ZetboxContextManagerModule.ZetboxContext, i)).ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }
    }
}